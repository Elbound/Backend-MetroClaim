using MetroClaim.Api.Data;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MetroClaim.Api.Repositories.Data;

public class UserLimitRepository : Repository<UserLimit>, IUserLimitRepository
{
    private readonly MetroClaimApiDbContext _context;
    public UserLimitRepository(MetroClaimApiDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UserLimit?> GetByUserAndCategoryAsync(Guid userId, Guid categoryId, CancellationToken cancellationToken)
    {
        return await _context.UserLimits
            .Include(ul => ul.Category) // Penting untuk cek limit max
            .FirstOrDefaultAsync(ul => ul.UserId == userId && ul.CategoryId == categoryId, cancellationToken);
    }

    public async Task<IEnumerable<UserLimit>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.UserLimits
            .Include(ul => ul.Category)
            .Where(ul => ul.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task ResetAllLimitsAsync(CancellationToken cancellationToken)
    {
        await _context.UserLimits
            .ExecuteUpdateAsync(s => s.SetProperty(ul => ul.LimitUsed, 0), cancellationToken);
    }
}
