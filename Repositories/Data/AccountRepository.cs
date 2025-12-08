using MetroClaim.Api.Data;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories.Interfaces;

namespace MetroClaim.Api.Repositories.Data;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    public AccountRepository(MetroClaimApiDbContext context) : base(context)
    {
    }
}
