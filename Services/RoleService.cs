using MetroClaim.Api.DTOs.Role;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories;
using MetroClaim.Api.Repositories.Interfaces;
using MetroClaim.Api.Services.Interfaces;

namespace MetroClaim.Api.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task CreateRoleAsync(string name, CancellationToken cancellationToken)
    {
        var newRole = new Role
        {
            Id = Guid.NewGuid(),
            Name = name,
            CreatedAt = DateTime.UtcNow,
        };

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _roleRepository.CreateAsync(newRole, cancellationToken);
        }, cancellationToken);
    }

    public async Task DeleteRoleAsync(Guid id, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(id, cancellationToken);

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _roleRepository.DeleteAsync(role);
        }, cancellationToken);

    }

    public async Task<IEnumerable<RoleDTO>> GetAllRolesAsync(CancellationToken cancellationToken)
    {
        var getAllRoles = await _roleRepository.GetAllAsync(cancellationToken);

        var allRoles = getAllRoles.Select(r => new RoleDTO(
            r.Id,
            r.Name
        ));

        return allRoles;

    }

    public async Task<RoleDTO> GetRolebyIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var getRole = await _roleRepository.GetByIdAsync(id, cancellationToken);

        var role = new RoleDTO(
            getRole.Id,
            getRole.Name
        );

        return role;
    }

    public async Task UpdateRoleAsync(RoleDTO roleDTO, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(roleDTO.id, cancellationToken);

        role.Name = roleDTO.name;

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _roleRepository.UpdateAsync(role);
        }, cancellationToken);

    }

}
