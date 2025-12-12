using MetroClaim.Api.DTOs.Trip;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories;
using MetroClaim.Api.Repositories.Interfaces;
using MetroClaim.Api.Services.Interfaces;
using MetroClaim.Api.Utilities;

namespace MetroClaim.Api.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;
    private readonly IReimbursementRepository _reimbursementRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;

    public TripService(
        ITripRepository tripRepository,
        IReimbursementRepository reimbursementRepository,
        ICategoryRepository categoryRepository,
        IUserRepository userRepository,
        IUserContext userContext,
        IUnitOfWork unitOfWork)
    {
        _tripRepository = tripRepository;
        _reimbursementRepository = reimbursementRepository;
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
    }

    // =========================================================================
    // READ METHODS
    // =========================================================================

    public async Task<TripDetailDto> GetTripByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var trip = await _tripRepository.GetByIdWithDetailsAsync(id, cancellationToken);
        if (trip is null) throw new KeyNotFoundException($"Trip {id} not found.");
        
        // Security check: Manager sendiri, Admin, Finance, atau Peserta trip tersebut
        var userId = _userContext.CurrentUserId;
        var isManager = trip.UserId == userId;
        var isParticipant = trip.Reimbursements.Any(r => r.UserId == userId);
        var isAdminOrFinance = _userContext.IsInRole("Admin") || _userContext.IsInRole("Finance");

        if (!isManager && !isParticipant && !isAdminOrFinance)
        {
            throw new UnauthorizedAccessException("You are not authorized to view this trip.");
        }

        return MapToDetailDto(trip);
    }

    public async Task<IEnumerable<TripDetailDto>> GetTripsCreatedByMeAsync(CancellationToken cancellationToken)
    {
        var managerId = _userContext.CurrentUserId;
        var trips = await _tripRepository.GetByManagerIdAsync(managerId, cancellationToken);
        return trips.Select(MapToDetailDto);
    }

    public async Task<IEnumerable<TripDetailDto>> GetMyAssignedTripsAsync(CancellationToken cancellationToken)
    {
        var userId = _userContext.CurrentUserId;
        var trips = await _tripRepository.GetByParticipantIdAsync(userId, cancellationToken);
        return trips.Select(MapToDetailDto);
    }

    public async Task<IEnumerable<TripDetailDto>> GetTripsForFinanceAsync(CancellationToken cancellationToken)
    {
        if (!_userContext.IsInRole("Finance")) throw new UnauthorizedAccessException();
        var trips = await _tripRepository.GetForFinanceAsync(cancellationToken);
        return trips.Select(MapToDetailDto);
    }

    // =========================================================================
    // WRITE METHODS
    // =========================================================================

    public async Task<TripDetailDto> CreateTripAsync(CreateTripRequestDto requestDto, CancellationToken cancellationToken)
    {
        var managerId = _userContext.CurrentUserId;
        
        // 1. Validasi Kategori (Untuk Reimbursement)
        var category = await _categoryRepository.GetByIdAsync(requestDto.CategoryId, cancellationToken);
        if (category is null) throw new KeyNotFoundException("Category not found.");

        // 2. Validasi Peserta
        if (!requestDto.ParticipantIds.Any())
            throw new ArgumentException("At least one participant is required.");

        // 3. Create Trip Header
        var tripId = Guid.NewGuid();
        var now = DateTime.UtcNow;

        var newTrip = new Trip
        {
            Id = tripId,
            UserId = managerId,
            Title = requestDto.Title,
            Description = requestDto.Description,
            Destination = requestDto.Destination,
            StartDate = requestDto.StartDate,
            EndDate = requestDto.EndDate,
            Cost = 0,
            TripStatus = TripStatus.ManagerSubmited,
            CreatedAt = now,
            UpdatedAt = now
        };

        // 4. Auto-Generate Reimbursements (Participants)
        var reimbursements = CreateReimbursementsForTrip(newTrip, requestDto.CategoryId, requestDto.ParticipantIds, now);
        
        // Attach to Graph
        foreach (var r in reimbursements) newTrip.Reimbursements.Add(r);

        // 5. Commit
        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _tripRepository.CreateAsync(newTrip, cancellationToken);
        }, cancellationToken);

        return MapToDetailDto(newTrip);
    }

    public async Task UpdateTripAsync(Guid id, UpdateTripRequestDto requestDto, CancellationToken cancellationToken)
    {
        // 1. Load Data dengan Reimbursement untuk sinkronisasi peserta
        var trip = await _tripRepository.GetByIdWithDetailsAsync(id, cancellationToken);
        if (trip is null) throw new KeyNotFoundException($"Trip {id} not found.");

        // 2. Validasi Akses & Status
        if (trip.UserId != _userContext.CurrentUserId) throw new UnauthorizedAccessException();
        
        if (trip.TripStatus != TripStatus.ManagerSubmited)
        {
            throw new InvalidOperationException("Cannot update trip after it has been processed by Finance.");
        }

        // 3. Update Scalar Data
        
        bool categoryChanged = trip.Reimbursements.FirstOrDefault()?.CategoryId != requestDto.CategoryId;
        // Asumsi semua rimbursement di trip yang sama punya kategori sama. 
        // Logic: Jika user mengganti CategoryId di Trip, kita harus update semua Reimbursement yang belum diproses.

        trip.Title = requestDto.Title;
        trip.Description = requestDto.Description;
        trip.Destination = requestDto.Destination;
        trip.StartDate = requestDto.StartDate;
        trip.EndDate = requestDto.EndDate;
        trip.UpdatedAt = DateTime.UtcNow;

        if (categoryChanged)
        {
            foreach (var r in trip.Reimbursements.Where(x => x.ReimbursementStatus == ReimbursementStatus.Pending))
            {
                r.CategoryId = requestDto.CategoryId;
                r.UpdatedAt = DateTime.UtcNow;
            }
        }

        // 4. SYNC PARTICIPANTS Logic (Advanced)
        // Kita bandingkan list peserta lama (existing) dengan list baru (requestDto)
        
        var existingParticipantIds = trip.Reimbursements.Select(r => r.UserId).ToList();
        var newParticipantIds = requestDto.ParticipantIds;

        // A. Peserta yang DIHAPUS (Ada di existing, tapi tidak ada di request)
        var usersToRemove = existingParticipantIds.Except(newParticipantIds).ToList();
        
        // B. Peserta yang DITAMBAH (Tidak ada di existing, ada di request)
        var usersToAdd = newParticipantIds.Except(existingParticipantIds).ToList();

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            // A. Hapus Reimbursement untuk user yang diremove
            foreach (var userId in usersToRemove)
            {
                var reimbursementToRemove = trip.Reimbursements.FirstOrDefault(r => r.UserId == userId);
                if (reimbursementToRemove != null)
                {
                    // Hanya boleh hapus jika status masih Pending (belum diisi/diapprove)
                    if (reimbursementToRemove.ReimbursementStatus == ReimbursementStatus.Pending)
                    {
                        await _reimbursementRepository.DeleteAsync(reimbursementToRemove);
                    }
                }
            }

            // B. Buat Reimbursement untuk user yang ditambah
            if (usersToAdd.Any())
            {
                var newReimbursements = CreateReimbursementsForTrip(trip, requestDto.CategoryId, usersToAdd, DateTime.UtcNow);
                foreach (var nr in newReimbursements)
                {
                    await _reimbursementRepository.CreateAsync(nr, cancellationToken);
                }
            }

            // C. Update Trip Header
            await _tripRepository.UpdateAsync(trip);

        }, cancellationToken);
    }

    public async Task ReviewTripByFinanceAsync(Guid id, FinanceReviewTripDto requestDto, CancellationToken cancellationToken)
    {
        if (!_userContext.IsInRole("Finance")) throw new UnauthorizedAccessException();

        var trip = await _tripRepository.GetByIdAsync(id, cancellationToken);
        if (trip is null) throw new KeyNotFoundException("Trip not found.");

        if (trip.TripStatus != TripStatus.ManagerSubmited)
            throw new InvalidOperationException("Trip is not in submitted state.");

        if (requestDto.IsApproved)
        {
            trip.Cost = requestDto.AllocatedCost;
            trip.TripStatus = TripStatus.FinanceApproved;
        }
        else
        {
            trip.TripStatus = TripStatus.Canceled;
            // Note: Jika Canceled, reimbursement terkait tetap ada tapi status trip-nya Canceled.
            // Bisa tambahkan logic untuk cancel reimbursement juga jika perlu.
        }
        
        trip.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _tripRepository.UpdateAsync(trip);
        }, cancellationToken);
    }

    public async Task PublishTripAsync(Guid id, CancellationToken cancellationToken)
    {
        var trip = await _tripRepository.GetByIdAsync(id, cancellationToken);
        if (trip is null) throw new KeyNotFoundException("Trip not found.");

        if (trip.TripStatus != TripStatus.FinanceApproved)
            throw new InvalidOperationException("Trip must be approved by Finance first.");

        // Ubah Status
        trip.TripStatus = TripStatus.Ongoing;
        trip.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _tripRepository.UpdateAsync(trip);
        }, cancellationToken);
    }

    public async Task CancelTripAsync(Guid id, CancellationToken cancellationToken)
    {
        var trip = await _tripRepository.GetByIdAsync(id, cancellationToken);
        if (trip is null) throw new KeyNotFoundException("Trip not found.");

        if (trip.UserId != _userContext.CurrentUserId && !_userContext.IsInRole("Admin"))
            throw new UnauthorizedAccessException();

        trip.TripStatus = TripStatus.Canceled;
        trip.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _tripRepository.UpdateAsync(trip);
        }, cancellationToken);
    }

    // =========================================================================
    // PRIVATE HELPERS
    // =========================================================================

    private List<Reimbursement> CreateReimbursementsForTrip(Trip trip, Guid categoryId, List<Guid> participantIds, DateTime now)
    {
        var list = new List<Reimbursement>();

        foreach (var userId in participantIds)
        {
            var rId = Guid.NewGuid();
            var r = new Reimbursement
            {
                Id = rId,
                UserId = userId,
                CategoryId = categoryId,
                TripId = trip.Id,
                Title = $"Trip Expense: {trip.Destination}",
                Description = "Auto-generated reimbursement for business trip. Please update with your expenses.",
                TotalAmount = 0,
                ReimbursementStatus = ReimbursementStatus.Pending,
                CreatedAt = now,
                UpdatedAt = now
            };

            // ADD LOG: Drafted
            r.ApprovalLogs.Add(new ApprovalLog
            {
                Id = Guid.NewGuid(),
                ReimbursementId = rId,
                UserId = trip.UserId, // Created by Manager
                ApprovalLogStatus = ApprovalLogStatus.Drafted, // Agar bisa diedit User
                Comment = "System generated from Trip",
                CreatedAt = now,
                UpdatedAt = now
            });

            list.Add(r);
        }
        return list;
    }

    private TripDetailDto MapToDetailDto(Trip trip)
    {
        return new TripDetailDto(
            trip.Id,
            trip.Title ?? "-",
            trip.Description ?? "-",
            trip.Destination ?? "-",
            trip.StartDate,
            trip.EndDate,
            trip.Cost,
            trip.TripStatus.ToString(),
            trip.User?.FullName ?? "Unknown Manager",
            trip.CreatedAt,
            // Mapping Participants diambil dari Reimbursements yang terhubung
            trip.Reimbursements.Select(r => new TripParticipantDto(
                r.UserId,
                r.User?.FullName ?? "Loading...",
                r.ReimbursementStatus.ToString(),
                r.TotalAmount
            )).ToList()
        );
    }
}