using MetroClaim.Api.DTOs.UserLimit;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories;
using MetroClaim.Api.Repositories.Interfaces;
using MetroClaim.Api.Services.Interfaces;

namespace MetroClaim.Api.Services;

public class UserLimitService : IUserLimitService
{
    private readonly IUserLimitRepository _userLimitRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;

    public UserLimitService(
        IUserLimitRepository userLimitRepository,
        ICategoryRepository categoryRepository,
        IUserContext userContext,
        IUnitOfWork unitOfWork)
    {
        _userLimitRepository = userLimitRepository;
        _categoryRepository = categoryRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<UserLimitDto>> GetMyLimitsAsync(CancellationToken cancellationToken)
    {
        var userId = _userContext.CurrentUserId;
        if (userId == Guid.Empty) throw new UnauthorizedAccessException();

        var limits = await _userLimitRepository.GetAllByUserIdAsync(userId, cancellationToken);

        return limits.Select(ul => new UserLimitDto(
            ul.Id,
            ul.CategoryId,
            ul.Category?.Name ?? "Unknown",
            ul.Category?.Limit ?? 0,
            ul.LimitUsed,
            (ul.Category?.Limit ?? 0) - ul.LimitUsed
        ));
    }

    public async Task CreateUserLimitAsync(CreateUserLimitRequestDto requestDto, CancellationToken cancellationToken)
    {
        var userId = _userContext.CurrentUserId;
        if (userId == Guid.Empty) throw new UnauthorizedAccessException();

        var category = await _categoryRepository.GetByIdAsync(requestDto.CategoryId, cancellationToken);
        if (category is null)
        {
            throw new KeyNotFoundException($"Category with ID {requestDto.CategoryId} not found.");
        }

        if (requestDto.CategoryId == Guid.Parse("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"))
        {
            throw new InvalidOperationException("Cannot manually create limit for System Reserved Category (Trip).");
        }

        var existingLimit = await _userLimitRepository.GetByUserAndCategoryAsync(userId, requestDto.CategoryId, cancellationToken);
        if (existingLimit is not null)
        {
            throw new InvalidOperationException($"You already have a limit set for category '{category.Name}'.");
        }

        var newUserLimit = new UserLimit
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CategoryId = requestDto.CategoryId,
            LimitUsed = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _userLimitRepository.CreateAsync(newUserLimit, cancellationToken);
        }, cancellationToken);
    }
}