using MetroClaim.Api.Data;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories.Interfaces;

namespace MetroClaim.Api.Repositories.Data;

public class ApprovalLogRepository : Repository<ApprovalLog>, IApprovalLogRepository
{
    public ApprovalLogRepository(MetroClaimApiDbContext context) : base(context)
    {
    }
}
