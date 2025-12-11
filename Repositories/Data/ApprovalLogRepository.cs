using MetroClaim.Api.Data;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MetroClaim.Api.Repositories.Data;

public class ApprovalLogRepository : Repository<ApprovalLog>, IApprovalLogRepository
{
    private readonly MetroClaimApiDbContext _context;
    public ApprovalLogRepository(MetroClaimApiDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ApprovalLog>> GetAllByReimbursementId(Guid id,CancellationToken cancellationToken)
    {
        return await _context.Set<ApprovalLog>().Where(al => al.ReimbursementId == id).ToListAsync();
    }
}
