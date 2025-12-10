using System.Security.Claims;
using MetroClaim.Api.DTOs.Auth;
using MetroClaim.Api.Repositories;
using MetroClaim.Api.Repositories.Interfaces;
using MetroClaim.Api.Services.Interfaces;
using MetroClaim.Api.Utilities;

namespace MetroClaim.Api.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IHashHandler _hashHandler;
    private readonly ITokenHandler _tokenHandler;

    public AuthService(IUserRepository userRepository, IAccountRepository accountRepository, IHashHandler hashHandler, ITokenHandler tokenHandler)
    {
        _userRepository = userRepository;
        _accountRepository = accountRepository;
        _hashHandler = hashHandler;
        _tokenHandler = tokenHandler;
    }

    public async Task<string> LoginAsync(AuthLoginRequestDto requestDto, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByEmailAsync(requestDto.Email, cancellationToken);

        if (account is null || !_hashHandler.ValidateHash(requestDto.Password, account.Password!))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        if (!account.IsActive)
        {
            throw new UnauthorizedAccessException("Your account is deactivated. Please contact admin.");
        }

        var user = await _userRepository.GetByIdAsync(account.UserId, cancellationToken);

        if (user is null)
        {
            throw new KeyNotFoundException("User profile not found.");
        }

        var claims = new List<Claim>();
        
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Name, user.FullName ?? "Unknown"));
        claims.Add(new Claim(ClaimTypes.Email, account.Email!));

        if (user.UserRoles is not null && user.UserRoles.Any())
        {
            foreach (var userRole in user.UserRoles)
            {
                if (userRole.Role is not null && !string.IsNullOrEmpty(userRole.Role.Name))
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
                }
            }
        }
        else 
        {
            claims.Add(new Claim(ClaimTypes.Role, "Employee")); 
        }

        var token = _tokenHandler.Access(claims);

        return token;
    }
}
