using System.Data.Common;
using MetroClaim.Api.Data;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MetroClaim.Api.Repositories.Data;

public class TripRepository : Repository<Trip>, ITripRepository
{
    private readonly MetroClaimApiDbContext _context;
    public TripRepository(MetroClaimApiDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Trip?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Trips
            .Include(t => t.User) // Manager info
            .Include(t => t.Reimbursements) // Participants info
                .ThenInclude(r => r.User)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Trip>> GetByManagerIdAsync(Guid managerId, CancellationToken cancellationToken)
    {
        return await _context.Trips
            .Include(t => t.User)
            .Include(t => t.Reimbursements)
                .ThenInclude(r => r.User)
            .Where(t => t.UserId == managerId)
            .OrderByDescending(t => t.CreatedAt)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Trip>> GetByParticipantIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        // Query: Cari Trip yang memiliki Reimbursement milik UserID ini
        return await _context.Trips
            .Include(t => t.User)
            .Include(t => t.Reimbursements.Where(r => r.UserId == userId)) // Filter child agar ringan
            .Where(t => t.Reimbursements.Any(r => r.UserId == userId))
            .OrderByDescending(t => t.CreatedAt)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Trip>> GetForFinanceAsync(CancellationToken cancellationToken)
    {
        return await _context.Trips
            .Include(t => t.User)
            .Include(t => t.Reimbursements)
                .ThenInclude(r => r.User)
            .Where(t => t.TripStatus == TripStatus.ManagerSubmited || 
                        t.TripStatus == TripStatus.FinanceApproved)
            .OrderBy(t => t.CreatedAt) // FIFO
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    public async Task<Trip?> GetByIdWithParticipantsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Trips
            .Include(t => t.Reimbursements) // Penting: Load reimbursement untuk hitung Total Usage
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
}
