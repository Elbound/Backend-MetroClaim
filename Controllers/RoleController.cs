using MetroClaim.Api.DTOs.Role;
using MetroClaim.Api.Services.Interfaces;
using MetroClaim.Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetroClaim.Api.Controllers;

[ApiController]
[Route("api/Role")]
[Authorize]
public class RoleController:ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        var allRoles = await _roleService.GetAllRolesAsync(cancellationToken);
        return Ok(new ApiResponse<object>(allRoles));
    }

    [HttpGet("{id}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetRoleById(Guid id, CancellationToken cancellationToken)
    {
        var role = await _roleService.GetRolebyIdAsync(id, cancellationToken);
        return Ok(new ApiResponse<object>(role));
    }

    [HttpDelete]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteRole(Guid id, CancellationToken cancellationToken)
    {
        await _roleService.DeleteRoleAsync(id, cancellationToken);
        return Ok(new ApiResponse<object>("Role Deleted"));
    }

    [HttpPost]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateRole(string name, CancellationToken cancellationToken)
    {
        await _roleService.CreateRoleAsync(name, cancellationToken);
        return Ok(new ApiResponse<object>("Role Created"));
    }

    [HttpPut]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateRole(RoleDTO roleDTO, CancellationToken cancellationToken)
    {
        await _roleService.UpdateRoleAsync(roleDTO, cancellationToken);
        return Ok(new ApiResponse<object>("Role Updated"));
    }
}
