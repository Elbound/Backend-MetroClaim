using MetroClaim.Api.Data;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MetroClaim.Api.Repositories.Data;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    private readonly MetroClaimApiDbContext _context;
    public AccountRepository(MetroClaimApiDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Account?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email, cancellationToken);
    }

    public async Task<Account?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == userId, cancellationToken);
    }
}
