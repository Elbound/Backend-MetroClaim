using MetroClaim.Api.DTOs.UserLimit;
using MetroClaim.Api.Services;
using MetroClaim.Api.Services.Interfaces;
using MetroClaim.Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetroClaim.Api.Controllers;

[ApiController]
[Route("api/user-limit")]
[Authorize]
public class UserLimitController : ControllerBase
{
    private readonly IUserLimitService _userLimitService;

    public UserLimitController(IUserLimitService userLimitService)
    {
        _userLimitService = userLimitService;
    }

    [HttpPost]
    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> CreateUserLimit(CreateUserLimitRequestDto requestDto, CancellationToken cancellationToken)
    {
        await _userLimitService.CreateUserLimitAsync(requestDto, cancellationToken);
        return Ok(new ApiResponse<object>("user limit generated"));
    }

    [HttpGet]
    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> GetUserLimit(CancellationToken cancellationToken)
    {
        var userLimits = await _userLimitService.GetMyLimitsAsync(cancellationToken);
        return Ok(new ApiResponse<IEnumerable<UserLimitDto>>(userLimits));
    }
}
