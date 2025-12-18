using MetroClaim.Api.Models;

namespace MetroClaim.Api.Repositories.Interfaces;

public interface IUserLimitRepository : IRepository<UserLimit>
{
    Task<UserLimit?> GetByUserAndCategoryAsync(Guid userId, Guid categoryId, CancellationToken cancellationToken);
    Task<IEnumerable<UserLimit>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task ResetAllLimitsAsync(CancellationToken cancellationToken);
}
