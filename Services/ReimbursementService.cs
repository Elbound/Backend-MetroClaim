using MetroClaim.Api.DTOs.ApprovalLog;
using MetroClaim.Api.DTOs.Reimbursement;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories;
using MetroClaim.Api.Repositories.Interfaces;
using MetroClaim.Api.Services.Interfaces;

namespace MetroClaim.Api.Services;

public class ReimbursementService : IReimbursementService
{
    private readonly IReimbursementRepository _reimbursementRepository;
    private readonly IUserLimitRepository _userLimitRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITripRepository _tripRepository;
    private readonly IApprovalLogRepository _approvalLogRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReimbursementItemRepository _reimbursementItemRepository;

    public ReimbursementService(
        IReimbursementRepository reimbursementRepository,
        IUserLimitRepository userLimitRepository,
        ICategoryRepository categoryRepository,
        ITripRepository tripRepository,
        IApprovalLogRepository approvalLogRepository,
        IReimbursementItemRepository reimbursementItemRepository,
        IUserRepository userRepository,
        IUserContext userContext,
        IUnitOfWork unitOfWork)
    {
        _reimbursementRepository = reimbursementRepository;
        _userLimitRepository = userLimitRepository;
        _categoryRepository = categoryRepository;
        _tripRepository = tripRepository;
        _approvalLogRepository = approvalLogRepository;
        _userRepository = userRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
        _reimbursementItemRepository = reimbursementItemRepository;
    }

    public async Task<ReimbursementDetailDto> CreateReimbursementAsync(ReimbursementCreateRequestDto requestDto, CancellationToken cancellationToken)
    {
        // 1. Validasi User
        var currentUserId = _userContext.CurrentUserId;
        if (currentUserId == Guid.Empty) throw new UnauthorizedAccessException("User is not authenticated.");

        // 2. Validasi Item
        if (requestDto.Items == null || !requestDto.Items.Any())
        {
            throw new ArgumentException("Reimbursement must have at least one item.");
        }

        // 3. Hitung Total & Validasi Limit
        decimal calculatedTotal = requestDto.Items.Sum(x => x.Amount);

        var userLimit = await _userLimitRepository.GetByUserAndCategoryAsync(currentUserId, requestDto.CategoryId, cancellationToken);

        if (userLimit is null)
        {
            throw new InvalidOperationException("You have not set up a limit for this category. Please create a user limit first.");
        }

        decimal maxLimit = userLimit.Category!.Limit;
        decimal projectedUsage = userLimit.LimitUsed + calculatedTotal;

        if (projectedUsage > maxLimit)
        {
            decimal remaining = maxLimit - userLimit.LimitUsed;
            throw new InvalidOperationException($"Insufficient limit balance. Remaining: {remaining:N2}, Requested: {calculatedTotal:N2}");
        }

        // Update Limit State (In-Memory)
        userLimit.LimitUsed = projectedUsage;
        userLimit.UpdatedAt = DateTime.UtcNow;

        // 4. Cek Trip
        Trip? trip = null;
        string? tripTitle = null;
        if (requestDto.TripId.HasValue)
        {
            trip = await _tripRepository.GetByIdAsync(requestDto.TripId.Value, cancellationToken);
            if (trip is null) throw new KeyNotFoundException($"Trip with ID {requestDto.TripId} not found.");
            tripTitle = trip.Title;
        }

        // 5. Setup ID & Timestamp
        var reimbursementId = Guid.NewGuid();
        var now = DateTime.UtcNow;

        // 6. Buat Header Reimbursement
        var reimbursement = new Reimbursement
        {
            Id = reimbursementId,
            UserId = currentUserId,
            CategoryId = requestDto.CategoryId,
            TripId = requestDto.TripId,
            Title = requestDto.Title,
            Description = requestDto.Description,
            TotalAmount = calculatedTotal,
            ReimbursementStatus = ReimbursementStatus.Pending,
            CreatedAt = now,
            UpdatedAt = now
        };

        // 7. Buat Items
        var itemsList = requestDto.Items.Select(i => new ReimbursementItem
        {
            Id = Guid.NewGuid(),
            ReimbursementId = reimbursementId,
            Amount = i.Amount,
            DateOfExpense = i.DateOfExpense,
            Receipt = i.Receipt,
            CreatedAt = now,
            UpdatedAt = now
        }).ToList();

        // 8. [BARU] Buat Initial Approval Log (Submitted)
        // Ini penting agar Manager bisa melihat data ini di list approval mereka
        var initialLog = new ApprovalLog
        {
            Id = Guid.NewGuid(),
            ReimbursementId = reimbursementId,
            UserId = currentUserId, // User yang submit
            ApprovalLogStatus = ApprovalLogStatus.Submitted, // Status Trigger untuk Manager
            Comment = "Initial submission",
            CreatedAt = now,
            UpdatedAt = now
        };

        // 9. Attach ke Entity Graph
        reimbursement.Items = itemsList;
        reimbursement.ApprovalLogs.Add(initialLog); // Tambahkan log ke koleksi header

        // 10. Simpan Transaksi
        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _userLimitRepository.UpdateAsync(userLimit);

            await _reimbursementRepository.CreateAsync(reimbursement, cancellationToken);

        }, cancellationToken);

        reimbursement.User = new User 
        { 
            Id = currentUserId, 
            FullName = _userContext.CurrentName,
            EmployeeId = "-" // Not available in context
        };
        reimbursement.Category = userLimit.Category;
        reimbursement.Trip = trip;
        
        // Ensure Log has User for mapping
        initialLog.User = reimbursement.User; 

        return MapToDetailDto(reimbursement);
    }

    public async Task<IEnumerable<ReimbursemenGetResponseDto>> GetAllReimbursementsAsync(CancellationToken cancellationToken)
    {
        var reimbursements = await _reimbursementRepository.GetAllWithReferencesAsync(cancellationToken);

        return reimbursements.Select(r => new ReimbursemenGetResponseDto(
            r.Id,
            r.User?.EmployeeId ?? "-",
            r.User?.FullName ?? "Unknown",
            r.Category?.Name ?? "-",
            r.Trip?.Title,
            r.Title ?? "",
            r.Description ?? "",
            r.TotalAmount,
            r.ReimbursementStatus.ToString(),
            r.CreatedAt,
            r.UpdatedAt
        ));
    }

    public async Task<ReimbursementDetailDto> GetReimbursementByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var reimbursement = await _reimbursementRepository.GetByIdReadOnlyAsync(id, cancellationToken);

        if (reimbursement is null)
        {
            throw new KeyNotFoundException($"Reimbursement with ID {id} not found.");
        }

        var currentUserId = _userContext.CurrentUserId;
        var isAdmin = _userContext.IsInRole("Admin");
        var isFinance = _userContext.IsInRole("Finance");
        var isManager = _userContext.IsInRole("Manager");

        var isOwner = reimbursement.UserId == currentUserId;

        var isSubordinate = isManager && (reimbursement.User?.ManagerId == currentUserId);

        if (!isAdmin && !isFinance && !isOwner && !isSubordinate)
        {
            throw new UnauthorizedAccessException("You represent not authorized to view this reimbursement.");
        }

        return MapToDetailDto(reimbursement);
    }

    public async Task<IEnumerable<ReimbursementDetailDto>> GetSubordinateReimbursementsAsync(CancellationToken cancellationToken)
    {
        var managerId = _userContext.CurrentUserId;

        if (!_userContext.IsInRole("Manager"))
        {
            throw new UnauthorizedAccessException("Access denied. Manager role required.");
        }

        var reimbursements = await _reimbursementRepository.GetPendingForManagerAsync(managerId, cancellationToken);

        return reimbursements.Select(MapToDetailDto);
    }


    public async Task<IEnumerable<ReimbursementDetailDto>> GetMyReimbursementsAsync(CancellationToken cancellationToken)
    {
        var currentUserId = _userContext.CurrentUserId;
        if (currentUserId == Guid.Empty) throw new UnauthorizedAccessException();

        var reimbursements = await _reimbursementRepository.GetByUserIdWithDetailsAsync(currentUserId, cancellationToken);

        return reimbursements.Select(MapToDetailDto);
    }

    public async Task<IEnumerable<ReimbursementDetailDto>> GetForFinanceAsync(CancellationToken cancellationToken)
    {
        if (!_userContext.IsInRole("Finance"))
        {
            throw new UnauthorizedAccessException("Access denied. Finance role required.");
        }

        var reimbursements = await _reimbursementRepository.GetPendingForFinanceAsync(cancellationToken);

        return reimbursements.Select(MapToDetailDto);
    }

    private ReimbursementDetailDto MapToDetailDto(Reimbursement r)
    {
        return new ReimbursementDetailDto(
            r.Id,
            r.User?.EmployeeId ?? "-",
            r.User?.FullName ?? "Unknown",
            r.Category?.Name ?? "-",
            r.Trip?.Title,
            r.Title ?? "",
            r.Description ?? "",
            r.TotalAmount,
            r.ReimbursementStatus.ToString(),
            r.CreatedAt,
            r.UpdatedAt,
            r.Items.Select(i => new ReimbursementItemDto(
                i.Id,
                i.Amount,
                i.DateOfExpense,
                i.Receipt
            )).ToList(),
            r.ApprovalLogs.OrderByDescending(l => l.CreatedAt)
                          .Select(l => new ApprovalLogDto(
                              l.Id,
                              l.User?.FullName ?? "System/Unknown",
                              l.ApprovalLogStatus.ToString(),
                              l.Comment,
                              l.CreatedAt
                          )).ToList()
        );
    }


    public async Task UpdateReimbursementAsync(Guid id, ReimbursementUpdateRequestDto requestDto, CancellationToken cancellationToken)
    {
        // 1. Ambil Data (Connected/Tracked)
        var reimbursement = await _reimbursementRepository.GetByIdForUpdateAsync(id, cancellationToken);
        if (reimbursement is null) throw new KeyNotFoundException($"Reimbursement {id} not found.");

        // 2. Validasi Kategori
        // Jika CategoryId di DTO null, gunakan existing. Jika ada value, validasi.
        Guid targetCategoryId = requestDto.CategoryId ?? reimbursement.CategoryId;
        Category? categoryCheck = null;

        if (requestDto.CategoryId.HasValue)
        {
             // Hanya validasi ke DB jika user mengirim perubahan kategori
             categoryCheck = await _categoryRepository.GetByIdAsync(requestDto.CategoryId.Value, cancellationToken);
             if (categoryCheck is null)
             {
                 throw new KeyNotFoundException($"Category with ID {requestDto.CategoryId} not found.");
             }
        }
        else
        {
             // Optional: Load category existing jika butuh nama untuk error message (opsional)
             // categoryCheck = reimbursement.Category; // Note: reimbursement loaded with .Include()
        }

        // 3. Validasi Akses
        var currentUserId = _userContext.CurrentUserId;
        if (reimbursement.UserId != currentUserId)
            throw new UnauthorizedAccessException("You can only edit your own reimbursement.");

        // 4. Validasi Status
        var lastLog = reimbursement.ApprovalLogs.OrderByDescending(x => x.CreatedAt).FirstOrDefault();
        bool isEditable = reimbursement.ReimbursementStatus == ReimbursementStatus.Pending &&
                          (lastLog == null ||
                           lastLog.ApprovalLogStatus == ApprovalLogStatus.Drafted ||
                           lastLog.ApprovalLogStatus == ApprovalLogStatus.ManagerRevision);

        if (!isEditable)
            throw new InvalidOperationException("Cannot edit reimbursement that is already submitted or processed.");

        // 5. Hitung Total Baru
        decimal newTotal = requestDto.Items.Sum(x => x.Amount);

        // 6. Logic Limit
        if (reimbursement.TripId.HasValue)
        {
            // CASE A: Trip
            var trip = await _tripRepository.GetByIdWithParticipantsAsync(reimbursement.TripId.Value, cancellationToken);

            if (trip != null)
            {
                decimal currentTripUsage = trip.Reimbursements
                    .Where(r => r.Id != id && r.ReimbursementStatus != ReimbursementStatus.Rejected)
                    .Sum(r => r.TotalAmount);

                if ((currentTripUsage + newTotal) > trip.Cost)
                {
                    decimal remaining = trip.Cost - currentTripUsage;
                    throw new InvalidOperationException($"Trip budget exceeded. Remaining: {remaining:N2}");
                }
            }
        }
        else
        {
            // CASE B: Personal
            var userLimit = await _userLimitRepository.GetByUserAndCategoryAsync(reimbursement.UserId, targetCategoryId, cancellationToken);

            if (userLimit is null)
            {
                // Fallback name check
                string catName = categoryCheck?.Name ?? "Unknown";
                throw new InvalidOperationException($"You don't have a limit set for category {catName}.");
            }

            if (reimbursement.CategoryId == targetCategoryId)
            {
                decimal oldAmount = reimbursement.Items.Sum(x => x.Amount);
                decimal projectedLimitUsage = userLimit.LimitUsed - oldAmount + newTotal;

                if (projectedLimitUsage > userLimit.Category!.Limit)
                {
                    throw new InvalidOperationException($"Personal category limit exceeded.");
                }

                // Direct Update to Tracked Entity
                userLimit.LimitUsed = projectedLimitUsage;
                userLimit.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                // Jika User Ganti Kategori (CategoryId di DTO != Existing)
                throw new InvalidOperationException("Changing category is not allowed via update. Please delete and recreate.");
            }
        }

        // 7. Update Header Property
        reimbursement.Title = requestDto.Title;
        reimbursement.Description = requestDto.Description;
        
        if (requestDto.CategoryId.HasValue)
        {
            reimbursement.CategoryId = requestDto.CategoryId.Value;
        }

        reimbursement.TotalAmount = newTotal;
        reimbursement.UpdatedAt = DateTime.UtcNow;

        // 8. Update Items (Refactored to Repository Pattern to avoid Concurrency/Tracking issues)
        var itemsToRemove = reimbursement.Items.ToList();
        foreach (var item in itemsToRemove)
        {
            await _reimbursementItemRepository.DeleteAsync(item);
        }
        
        // Do NOT Clear() collection, let tracking handle the deletes.
        // Do NOT Add() to collection, use CreateAsync instead.

        foreach (var itemDto in requestDto.Items)
        {
            var newItem = new ReimbursementItem
            {
                Id = Guid.NewGuid(),
                ReimbursementId = reimbursement.Id,
                Amount = itemDto.Amount,
                DateOfExpense = itemDto.DateOfExpense,
                Receipt = itemDto.Receipt,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            await _reimbursementItemRepository.CreateAsync(newItem, cancellationToken);
        }

        // 9. Tambah Log (Use Repository to avoid Collection Modification issues)
        var newLog = new ApprovalLog
        {
            Id = Guid.NewGuid(),
            ReimbursementId = reimbursement.Id,
            UserId = currentUserId,
            ApprovalLogStatus = ApprovalLogStatus.Submitted,
            Comment = "Reimbursement updated by user.",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        await _approvalLogRepository.CreateAsync(newLog, cancellationToken);

        // 10. Commit Changes
        try
        {
            await _unitOfWork.CommitTransactionAsync(async () =>
            {
                // Pure Connected Pattern with clean separation
                await Task.CompletedTask;
            }, cancellationToken);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException ex)
        {
            var entry = ex.Entries.FirstOrDefault();
            var entityName = entry?.Entity.GetType().Name ?? "Unknown";
            var state = entry?.State.ToString() ?? "Unknown";
            // Check for Id
            var idProp = entry?.Properties.FirstOrDefault(p => p.Metadata.Name == "Id");
            var idVal = idProp?.CurrentValue?.ToString() ?? "N/A";
            
            throw new Exception($"Concurrency Error Detected! Entity: {entityName}, State: {state}, ID: {idVal}. Details: {ex.Message}");
        }
    }

    public async Task DeleteReimbursementAsync(Guid id, CancellationToken cancellationToken)
    {
        var reimbursement = await _reimbursementRepository.GetByIdForUpdateAsync(id, cancellationToken);
        // ... validation ...

        // Logic Refund (Modifikasi)
        UserLimit? userLimitToUpdate = null;

        // HANYA REFUND JIKA BUKAN TRIP
        if (reimbursement.TripId == null)
        {
            //Guid userId, Guid categoryId, CancellationToken cancellationToken
            var userLimit = await _userLimitRepository.GetByUserAndCategoryAsync(reimbursement.UserId, reimbursement.CategoryId, cancellationToken);
            if (userLimit != null)
            {
                userLimit.LimitUsed -= reimbursement.TotalAmount;
                if (userLimit.LimitUsed < 0) userLimit.LimitUsed = 0;
                userLimitToUpdate = userLimit;
            }
        }

        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            if (userLimitToUpdate != null) await _userLimitRepository.UpdateAsync(userLimitToUpdate);
            await _reimbursementRepository.DeleteAsync(reimbursement);
        }, cancellationToken);
    }

    public async Task ProcessApprovalAsync(Guid id, ApprovalProcessDto dto, CancellationToken cancelationToken)
    {
        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            //
            // 1. Ambil reimbursement (Connected/Tracked)
            //
            var reimbursement = await _reimbursementRepository.GetByIdForUpdateAsync(id, cancelationToken);
            if (reimbursement is null)
                throw new KeyNotFoundException($"Reimbursement {id} not found.");

            var userId = _userContext.CurrentUserId;

            var lastLog = reimbursement.ApprovalLogs
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefault();

            var lastStatus = lastLog?.ApprovalLogStatus ?? ApprovalLogStatus.Drafted;


            //
            // 2. Tentukan role
            //
            var isManager = _userContext.IsInRole("Manager") &&
                            reimbursement.User?.ManagerId == userId;

            var isFinance = _userContext.IsInRole("Finance");

            if (!isManager && !isFinance)
                throw new UnauthorizedAccessException("You are not authorized.");


            //
            // 3. Tentukan status baru
            //
            ApprovalLogStatus newLogStatus;
            ReimbursementStatus newHeaderStatus;
            bool refundLimit = false;
            bool shouldAddDueReimbursement = false;

            if (isManager)
            {
                if (lastStatus != ApprovalLogStatus.Submitted)
                    throw new InvalidOperationException("Manager cannot process this reimbursement.");

                switch (dto.Action)
                {
                    case ApprovalAction.Approve:
                        newLogStatus = ApprovalLogStatus.ManagerApproved;
                        newHeaderStatus = ReimbursementStatus.Pending;
                        break;

                    case ApprovalAction.Revise:
                        newLogStatus = ApprovalLogStatus.ManagerRevision;
                        newHeaderStatus = ReimbursementStatus.Pending;
                        break;

                    case ApprovalAction.Reject:
                        newLogStatus = ApprovalLogStatus.ManagerRejected;
                        newHeaderStatus = ReimbursementStatus.Rejected;
                        refundLimit = true;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else  // FINANCE
            {
                if (lastStatus != ApprovalLogStatus.ManagerApproved)
                    throw new InvalidOperationException("Finance only processes ManagerApproved reimbursements.");

                if (dto.Action == ApprovalAction.Revise)
                    throw new InvalidOperationException("Finance cannot revise.");

                switch (dto.Action)
                {
                    case ApprovalAction.Approve:
                        newLogStatus = ApprovalLogStatus.FinanceApproved;
                        newHeaderStatus = ReimbursementStatus.Approved;
                        shouldAddDueReimbursement = true; // <<— IMPORTANT
                        break;

                    case ApprovalAction.Reject:
                        newLogStatus = ApprovalLogStatus.FinanceRejected;
                        newHeaderStatus = ReimbursementStatus.Rejected;
                        refundLimit = true;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }


            //
            // 4. Validasi komentar wajib (Reject/Revise)
            //
            if ((dto.Action == ApprovalAction.Reject || dto.Action == ApprovalAction.Revise)
                && string.IsNullOrWhiteSpace(dto.Comment))
                throw new ArgumentException("Comment is required.");


            //
            // 5. Insert Approval Log baru
            // Note: Kita bisa Add ke collection, atau via Repo. Karena Repo sudah bersih (no save), via Repo juga aman.
            // Biar konsisten dengan style sebelumnya (dan mungkin repo ada logic lain), kita pakai Repo Create.
            var newLog = new ApprovalLog
            {
                Id = Guid.NewGuid(),
                ReimbursementId = reimbursement.Id,
                UserId = userId,
                ApprovalLogStatus = newLogStatus,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _approvalLogRepository.CreateAsync(newLog, cancelationToken);


            //
            // 6. Update header reimbursement (Direct Modify Tracked Entity)
            //
            reimbursement.ReimbursementStatus = newHeaderStatus;
            reimbursement.UpdatedAt = DateTime.UtcNow;

            // NO Explicit UpdateAsync needed for tracked entity
            // NO Hacky nullification needed


            //
            // 7. Refund limit jika Reject
            //
            if (refundLimit)
            {
                var limit = await _userLimitRepository
                    .GetByUserAndCategoryAsync(reimbursement.UserId, reimbursement.CategoryId, cancelationToken);

                if (limit != null)
                {
                    limit.LimitUsed -= reimbursement.TotalAmount;

                    if (limit.LimitUsed < 0)
                        limit.LimitUsed = 0;

                    limit.UpdatedAt = DateTime.UtcNow;
                }
            }


            //
            // 8. Finance Approve → Tambah DueReimbursement ke User
            //
            if (shouldAddDueReimbursement)
            {
                // GetByIdAsync usually returns Tracked entity
                var user = await _userRepository.GetByIdAsync(reimbursement.UserId, cancelationToken);
                if (user != null)
                {
                    user.DueReimbursement += reimbursement.TotalAmount;
                    user.UpdatedAt = DateTime.UtcNow;
                    // user is Tracked, no need for UpdateAsync
                }
            }

            // Commit handled by UnitOfWork
        }, cancelationToken);
    }

}
