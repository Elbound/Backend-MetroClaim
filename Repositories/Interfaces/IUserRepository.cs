using MetroClaim.Api.Models;

namespace MetroClaim.Api.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserWithDetailsAsync(Guid id, CancellationToken cancellationToken);
    
    Task<IEnumerable<User>> GetAllUsersWithDetailsAsync(CancellationToken cancellationToken);
}
