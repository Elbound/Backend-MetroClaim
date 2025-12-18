using MetroClaim.Api.Models;

namespace MetroClaim.Api.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserWithDetailsAsync(Guid id, CancellationToken cancellationToken);
    
    Task<IEnumerable<User>> GetAllUsersWithDetailsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<User>> GetByManagerIdAsync(Guid managerId, CancellationToken cancellationToken);
    Task<IEnumerable<User>> GetUsersByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    Task ResetAllDueReimbursementsAsync(CancellationToken cancellationToken);
}
