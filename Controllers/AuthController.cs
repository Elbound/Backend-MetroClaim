using MetroClaim.Api.DTOs.Auth;
using MetroClaim.Api.Services.Interfaces;
using MetroClaim.Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetroClaim.Api.Controllers;

[ApiController]
[Route("api/auth")]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(AuthLoginRequestDto requestDto, CancellationToken cancellationToken)
    {
        var login = await _authService.LoginAsync(requestDto, cancellationToken);
        return Ok(new ApiResponse<object>(new
        {
            Token = login
        }));
    }
}
