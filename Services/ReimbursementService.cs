using System;
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
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;

    public ReimbursementService(
        IReimbursementRepository reimbursementRepository,
        IUserLimitRepository userLimitRepository,
        ICategoryRepository categoryRepository,
        ITripRepository tripRepository,
        IUserContext userContext,
        IUnitOfWork unitOfWork)
    {
        _reimbursementRepository = reimbursementRepository;
        _userLimitRepository = userLimitRepository;
        _categoryRepository = categoryRepository;
        _tripRepository = tripRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<ReimbursementDetailDto> CreateReimbursementAsync(ReimbursementCreateRequestDto requestDto, CancellationToken cancellationToken)
    {
        var currentUserId = _userContext.CurrentUserId;
        if (currentUserId == Guid.Empty) throw new UnauthorizedAccessException("User is not authenticated.");

        if (requestDto.Items == null || !requestDto.Items.Any())
        {
            throw new ArgumentException("Reimbursement must have at least one item.");
        }

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

        userLimit.LimitUsed = projectedUsage;
        userLimit.UpdatedAt = DateTime.UtcNow;


        string? tripTitle = null;
        if (requestDto.TripId.HasValue)
        {
            var trip = await _tripRepository.GetByIdAsync(requestDto.TripId.Value, cancellationToken);
            if (trip is null) throw new KeyNotFoundException($"Trip with ID {requestDto.TripId} not found.");
            tripTitle = trip.Title;
        }

        var reimbursementId = Guid.NewGuid();
        var now = DateTime.UtcNow;

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

        reimbursement.Items = itemsList;

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _userLimitRepository.UpdateAsync(userLimit);

            await _reimbursementRepository.CreateAsync(reimbursement, cancellationToken);

        }, cancellationToken);

        var categoryName = userLimit.Category.Name;

        return new ReimbursementDetailDto(
            reimbursement.Id,
            _userContext.CurrentEmail,
            _userContext.CurrentName,
            categoryName ?? "-",
            tripTitle,
            reimbursement.Title!,
            reimbursement.Description!,
            reimbursement.TotalAmount,
            reimbursement.ReimbursementStatus.ToString(),
            reimbursement.CreatedAt,
            reimbursement.UpdatedAt,
            itemsList.Select(i => new ReimbursementItemDto(i.Id, i.Amount, i.DateOfExpense, "Receipt Uploaded")).ToList(),
            new List<ApprovalLogDto>()
        );
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
        var reimbursement = await _reimbursementRepository.GetByIdWithDetailsAsync(id, cancellationToken);

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

        return new ReimbursementDetailDto(
            reimbursement.Id,
            reimbursement.User?.EmployeeId ?? "-",
            reimbursement.User?.FullName ?? "Unknown",
            reimbursement.Category?.Name ?? "-",
            reimbursement.Trip?.Title,
            reimbursement.Title ?? "",
            reimbursement.Description ?? "",
            reimbursement.TotalAmount,
            reimbursement.ReimbursementStatus.ToString(),
            reimbursement.CreatedAt,
            reimbursement.UpdatedAt,
            reimbursement.Items.Select(i => new ReimbursementItemDto(
                i.Id,
                i.Amount,
                i.DateOfExpense,
                i.Receipt
            )).ToList(),
            reimbursement.ApprovalLogs.Select(log => new ApprovalLogDto(
                log.Id,
                log.User?.FullName ?? "Unknown Approver",
                log.ApprovalLogStatus.ToString(),
                log.Comment,
                log.CreatedAt
            )).OrderBy(l => l.CreatedAt).ToList()
        );
    }

    public async Task<IEnumerable<ReimbursementDetailDto>> GetSubordinateReimbursementsAsync(CancellationToken cancellationToken)
    {
        var managerId = _userContext.CurrentUserId;

        if (!_userContext.IsInRole("Manager"))
        {
            throw new UnauthorizedAccessException("Access denied. Manager role required.");
        }

        var reimbursements = await _reimbursementRepository.GetPendingForManagerAsync(managerId, cancellationToken);

        return reimbursements.Select(r => new ReimbursementDetailDto(
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
            new List<ReimbursementItemDto>(),
            r.ApprovalLogs.OrderByDescending(l => l.CreatedAt)
                          .Select(log => new ApprovalLogDto(
                              log.Id,
                              log.User?.FullName ?? "-",
                              log.ApprovalLogStatus.ToString(),
                              log.Comment,
                              log.CreatedAt
                          )).ToList()
        ));
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

        // Mapping DTO
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
        var reimbursement = await _reimbursementRepository.GetByIdWithDetailsAsync(id, cancellationToken);

        if (reimbursement is null) throw new KeyNotFoundException($"Reimbursement {id} not found.");

        if (reimbursement.UserId != _userContext.CurrentUserId)
        {
            throw new UnauthorizedAccessException("You can only edit your own reimbursement.");
        }

        var lastLog = reimbursement.ApprovalLogs.OrderByDescending(x => x.CreatedAt).FirstOrDefault();

        bool isEditable = reimbursement.ReimbursementStatus == ReimbursementStatus.Pending &&
                          (lastLog == null ||
                           lastLog.ApprovalLogStatus == ApprovalLogStatus.Drafted ||
                           lastLog.ApprovalLogStatus == ApprovalLogStatus.ManagerRevision);

        if (!isEditable)
        {
            throw new InvalidOperationException("Cannot edit reimbursement that is already submitted or processed.");
        }

        reimbursement.Title = requestDto.Title;
        reimbursement.Description = requestDto.Description;
        reimbursement.CategoryId = requestDto.CategoryId;
        reimbursement.UpdatedAt = DateTime.UtcNow;

        decimal newTotal = 0;

        var newItems = new List<ReimbursementItem>();
        foreach (var itemDto in requestDto.Items)
        {
            newTotal += itemDto.Amount;
            newItems.Add(new ReimbursementItem
            {
                Id = Guid.NewGuid(),
                ReimbursementId = reimbursement.Id,
                Amount = itemDto.Amount,
                DateOfExpense = itemDto.DateOfExpense,
                Receipt = itemDto.Receipt,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }
        reimbursement.TotalAmount = newTotal;

        var userLimit = await _userLimitRepository.GetByUserAndCategoryAsync(reimbursement.UserId, requestDto.CategoryId, cancellationToken);
        if (userLimit is null) throw new InvalidOperationException("User limit not found for this category.");

        decimal oldAmount = reimbursement.Items.Sum(x => x.Amount);
        userLimit.LimitUsed -= oldAmount;

        if ((userLimit.LimitUsed + newTotal) > userLimit.Category!.Limit)
        {
            throw new InvalidOperationException("Updated amount exceeds category limit.");
        }
        userLimit.LimitUsed += newTotal;
        userLimit.UpdatedAt = DateTime.UtcNow;

        var submitLog = new ApprovalLog
        {
            Id = Guid.NewGuid(),
            ReimbursementId = reimbursement.Id,
            UserId = _userContext.CurrentUserId,
            ApprovalLogStatus = ApprovalLogStatus.Submitted,
            Comment = "Reimbursement form updated and submitted by user.",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _reimbursementRepository.UpdateAsync(reimbursement);

            if (reimbursement.Items.Any())
            {
                // _context.ReimbursementItems.RemoveRange(reimbursement.Items); 
            }

            await _userLimitRepository.UpdateAsync(userLimit);

        }, cancellationToken);
    }

    public Task DeleteReimbursementAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
