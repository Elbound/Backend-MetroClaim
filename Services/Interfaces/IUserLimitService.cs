using MetroClaim.Api.DTOs.UserLimit;

namespace MetroClaim.Api.Services.Interfaces;

public interface IUserLimitService
{
    Task<IEnumerable<UserLimitDto>> GetMyLimitsAsync(CancellationToken cancellationToken);

    Task CreateUserLimitAsync(CreateUserLimitRequestDto requestDto, CancellationToken cancellationToken);
}