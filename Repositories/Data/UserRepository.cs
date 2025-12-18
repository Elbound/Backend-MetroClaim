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

    public async Task<IEnumerable<User>> GetUsersByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.Account)
            .Where(u => ids.Contains(u.Id))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .Include(u => u.Manager)
            .Include(u => u.Account)
            .Where(u => u.UserRoles.Any(ur => ur.Role.Name == roleName))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task ResetAllDueReimbursementsAsync(CancellationToken cancellationToken)
    {
        await _context.Users
            .ExecuteUpdateAsync(s => s.SetProperty(u => u.DueReimbursement, 0), cancellationToken);
    }
}
