using MetroClaim.Api.DTOs.User;
using MetroClaim.Api.Services.Interfaces;
using MetroClaim.Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetroClaim.Api.Controllers;

[ApiController]
[Route("api/user")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    // [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetAllUser(CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllUserAsync(cancellationToken);
        return Ok(new ApiResponse<IEnumerable<UserGetResponseDto>>(users));
    }

    [HttpGet("{id}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByIdAsync(id, cancellationToken);
        return Ok(new ApiResponse<UserGetResponseDto>(user));
    }

    [HttpPost]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RegisterUser(UserCreateRequestDto requestDto, CancellationToken cancellationToken)
    {
        await _userService.RegisterUserAsync(requestDto, cancellationToken);
        return Ok(new ApiResponse<object>("user registered"));
    }

    [HttpPut("{id}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserUpdateRequestDto requestDto, CancellationToken cancellationToken)
    {

        await _userService.UpdateUserAsync(id, requestDto, cancellationToken);
        return Ok(new ApiResponse<object>("user updated"));
    }

    [HttpDelete("{id}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
    {

        await _userService.DeleteUserAsync(id, cancellationToken);
        return Ok(new ApiResponse<object>("user deleted"));
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe(CancellationToken cancellationToken)
    {
        var user = await _userService.GetCurrentUserAsync(cancellationToken);
        return Ok(new ApiResponse<UserGetResponseDto>(user));
    }

    [HttpGet("subordinates")]
    public async Task<IActionResult> GetMySubordinates(CancellationToken cancellationToken)
    {
        var users = await _userService.GetMySubordinatesAsync(cancellationToken);
        return Ok(new ApiResponse<IEnumerable<UserGetResponseDto>>(users));
    }
}
