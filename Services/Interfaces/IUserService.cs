using System;
using MetroClaim.Api.DTOs.User;

namespace MetroClaim.Api.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserGetResponseDto>> GetAllUserAsync(CancellationToken cancellationToken);
    Task<UserGetResponseDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    Task RegisterUserAsync(UserCreateRequestDto requestDto, CancellationToken cancellationToken);
    Task UpdateUserAsync(Guid id, UserUpdateRequestDto requestDto, CancellationToken cancellationToken);
    Task DeleteUserAsync(Guid id, CancellationToken cancellationToken);
}
