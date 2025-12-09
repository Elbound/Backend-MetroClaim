using MetroClaim.Api.Models;

namespace MetroClaim.Api.Repositories.Interfaces;

public interface IAccountRepository : IRepository<Account>
{
    Task<Account?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<Account?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
