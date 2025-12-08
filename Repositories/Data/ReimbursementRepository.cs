using MetroClaim.Api.Data;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories.Interfaces;

namespace MetroClaim.Api.Repositories.Data;

public class ReimbursementRepository : Repository<Reimbursement>, IReimbursementRepository
{
    public ReimbursementRepository(MetroClaimApiDbContext context) : base(context)
    {
    }
}
