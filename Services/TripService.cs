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

    public async Task CreateTripAsync(CreateTripRequestDto requestDto, CancellationToken cancellationToken)
    {
        var managerId = _userContext.CurrentUserId;

        var categoryId = Guid.Parse("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"); // move this to constanta

        var category = await _categoryRepository.GetByIdAsync(categoryId, cancellationToken);
        
        if (category is null)
        {
            throw new ArgumentException("Category not found.");
        }

        if (!requestDto.ParticipantIds.Any())
        {
            throw new ArgumentException("At least one participant is required.");
        }

        if (requestDto.ParticipantIds.Count != requestDto.ParticipantIds.Distinct().Count())
        {
            throw new ArgumentException("Duplicate participants detected in the request.");
        }

        var existingUsers = await _userRepository.GetUsersByIdsAsync(requestDto.ParticipantIds, cancellationToken);
        if (existingUsers.Count() != requestDto.ParticipantIds.Distinct().Count())
        {
             var foundIds = existingUsers.Select(u => u.Id).ToHashSet();
             var missingIds = requestDto.ParticipantIds.Where(id => !foundIds.Contains(id));
             throw new ArgumentException($"Participants not found: {string.Join(", ", missingIds)}");
        }

        if (requestDto.EndDate < requestDto.StartDate)
        {
            throw new ArgumentException("End date cannot be earlier than start date.");
        }

        if (requestDto.StartDate.Date < DateTime.UtcNow.Date)
        {
            throw new ArgumentException("Start date cannot be in the past.");
        }

        var conflictingUserIds = await _tripRepository.GetConflictingUserIdsAsync(
            requestDto.ParticipantIds, 
            requestDto.StartDate, 
            requestDto.EndDate, 
            null, 
            cancellationToken);

        if (conflictingUserIds.Any())
        {
            var conflictingUsers = await _userRepository.GetUsersByIdsAsync(conflictingUserIds, cancellationToken);
            var names = string.Join(", ", conflictingUsers.Select(u => u.FullName));
            throw new ArgumentException($"The following users have conflicting trips: {names}");
        }

        var tripId = Guid.NewGuid();

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
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var reimbursements = CreateReimbursementsForTrip(newTrip, categoryId, requestDto.ParticipantIds, DateTime.UtcNow);
        
        foreach (var r in reimbursements) newTrip.Reimbursements.Add(r);

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _tripRepository.CreateAsync(newTrip, cancellationToken);
        }, cancellationToken);
    }


    public async Task<TripDetailDto> GetTripByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var trip = await _tripRepository.GetByIdWithDetailsAsync(id, cancellationToken);
        if (trip is null) throw new KeyNotFoundException($"Trip {id} not found.");
        
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
            var reimbursementId = Guid.NewGuid();
            var reimbursement = new Reimbursement
            {
                Id = reimbursementId,
                UserId = userId,
                CategoryId = categoryId,
                TripId = trip.Id,
                Title = $"Business Trip Expense: {trip.Destination}",
                Description = "Auto-generated reimbursement for business trip. Please update with your expenses.",
                TotalAmount = 0,
                ReimbursementStatus = ReimbursementStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            reimbursement.ApprovalLogs.Add(new ApprovalLog
            {
                Id = Guid.NewGuid(),
                ReimbursementId = reimbursementId,
                UserId = trip.UserId,
                ApprovalLogStatus = ApprovalLogStatus.Drafted,
                Comment = "System auto generated from Trip",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            list.Add(reimbursement);
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