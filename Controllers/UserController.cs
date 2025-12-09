using MetroClaim.Api.DTOs.User;
using MetroClaim.Api.Services.Interfaces;
using MetroClaim.Api.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace MetroClaim.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUser(CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllUserAsync(cancellationToken);
        return Ok(new ApiResponse<IEnumerable<UserGetResponseDto>>(users));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByIdAsync(id, cancellationToken);
        return Ok(new ApiResponse<UserGetResponseDto>(user));
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(UserCreateRequestDto requestDto, CancellationToken cancellationToken)
    {
        await _userService.RegisterUserAsync(requestDto, cancellationToken);
        return Ok(new ApiResponse<object>("user registered"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserUpdateRequestDto requestDto, CancellationToken cancellationToken)
    {

        await _userService.UpdateUserAsync(id, requestDto, cancellationToken);
        return Ok(new ApiResponse<object>("user updated"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
    {

        await _userService.DeleteUserAsync(id, cancellationToken);
        return Ok(new ApiResponse<object>("user deleted"));
    }
}
