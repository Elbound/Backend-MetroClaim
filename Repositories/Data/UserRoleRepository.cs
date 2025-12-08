using MetroClaim.Api.Data;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories.Interfaces;

namespace MetroClaim.Api.Repositories.Data;

public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(MetroClaimApiDbContext context) : base(context)
    {
    }
}
