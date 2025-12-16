using MetroClaim.Api.Models;

namespace MetroClaim.Api.Repositories.Interfaces;

public interface IReimbursementRepository : IRepository<Reimbursement>
{
    Task<IEnumerable<Reimbursement>> GetAllWithReferencesAsync(CancellationToken cancellationToken);
    Task<Reimbursement?> GetByIdReadOnlyAsync(Guid id, CancellationToken cancellationToken);
    Task<Reimbursement?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Reimbursement>> GetPendingForManagerAsync(Guid managerId, CancellationToken cancellationToken);
    Task<IEnumerable<Reimbursement>> GetHistoryForManagerAsync(Guid managerId, CancellationToken cancellationToken);

    Task<IEnumerable<Reimbursement>> GetByUserIdWithDetailsAsync(Guid userId, CancellationToken cancellationToken);

    Task<IEnumerable<Reimbursement>> GetPendingForFinanceAsync(CancellationToken cancellationToken);
}
