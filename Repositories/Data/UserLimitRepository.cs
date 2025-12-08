using MetroClaim.Api.Data;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories.Interfaces;

namespace MetroClaim.Api.Repositories.Data;

public class UserLimitRepository : Repository<UserLimit>, IUserLimitRepository
{
    public UserLimitRepository(MetroClaimApiDbContext context) : base(context)
    {
    }
}
