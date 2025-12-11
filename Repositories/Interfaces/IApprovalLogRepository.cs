using MetroClaim.Api.Models;

namespace MetroClaim.Api.Repositories.Interfaces;

public interface IApprovalLogRepository : IRepository<ApprovalLog>
{

    Task<IEnumerable<ApprovalLog>> GetAllByReimbursementId(Guid id, CancellationToken cancellationToken);


}
