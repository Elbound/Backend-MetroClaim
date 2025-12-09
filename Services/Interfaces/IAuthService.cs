using MetroClaim.Api.DTOs.Auth;

namespace MetroClaim.Api.Services.Interfaces;

public interface IAuthService
{
    Task<string> LoginAsync(AuthLoginRequestDto requestDto, CancellationToken cancellationToken);

}
