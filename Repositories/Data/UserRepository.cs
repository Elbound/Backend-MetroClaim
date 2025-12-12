using MetroClaim.Api.Data;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MetroClaim.Api.Repositories.Data;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly MetroClaimApiDbContext _context;
    public UserRepository(MetroClaimApiDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User?> GetUserWithDetailsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Include(u => u.Manager)
            .Include(u => u.Account)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetAllUsersWithDetailsAsync(CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Include(u => u.Manager)
            .Include(u => u.Account)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> GetByManagerIdAsync(Guid managerId, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Include(u => u.Manager)
            .Include(u => u.Account)
            .Where(u => u.ManagerId == managerId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
