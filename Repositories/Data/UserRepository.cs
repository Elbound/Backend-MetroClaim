using MetroClaim.Api.Data;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories.Interfaces;

namespace MetroClaim.Api.Repositories.Data;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(MetroClaimApiDbContext context) : base(context)
    {
    }
}
