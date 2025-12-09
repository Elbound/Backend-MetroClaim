using MetroClaim.Api.DTOs.User;
using MetroClaim.Api.Services.Interfaces;

namespace MetroClaim.Api.Services;

public class UserService : IUserService
{
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserGetResponseDto>> GetAllUserAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RegisterUserAsync(UserCreateRequestDto requestDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, UserCreateRequestDto requestDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
