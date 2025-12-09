using MetroClaim.Api.DTOs.Role;

namespace MetroClaim.Api.Services.Interfaces;

public interface IRoleService
{
    Task CreateRoleAsync(string name, CancellationToken cancellationToken);
    Task DeleteRoleAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateRoleAsync(RoleDTO roleDTO, CancellationToken cancellationToken);

    Task<IEnumerable<RoleDTO>> GetAllRolesAsync(CancellationToken cancellationToken);
    Task<RoleDTO> GetRolebyIdAsync(Guid id, CancellationToken cancellationToken);
    

}
