using System;
using MetroClaim.Api.DTOs.User;

namespace MetroClaim.Api.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserGetResponseDto>> GetAllUserAsync(CancellationToken cancellationToken);
    Task RegisterUserAsync(UserCreateRequestDto requestDto, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, UserCreateRequestDto requestDto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
