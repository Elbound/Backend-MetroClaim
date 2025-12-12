using MetroClaim.Api.Data;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MetroClaim.Api.Repositories.Data;

public class ReimbursementRepository : Repository<Reimbursement>, IReimbursementRepository
{
    private readonly MetroClaimApiDbContext _context;
    public ReimbursementRepository(MetroClaimApiDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Reimbursement>> GetAllWithReferencesAsync(CancellationToken cancellationToken)
    {
        return await _context.Reimbursements
            .Include(r => r.User)       // Join ke User
            .Include(r => r.Category)   // Join ke Category
            .Include(r => r.Trip)       // Join ke Trip
                                        // PENTING: Kita TIDAK Include(r => r.Items) di sini sesuai request
            .AsNoTracking()             // Optimization: Read-only lebih cepat
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Reimbursement?> GetByIdReadOnlyAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Reimbursements
            .Include(r => r.User)        // Info Pembuat
            .Include(r => r.Category)    // Info Kategori
            .Include(r => r.Trip)        // Info Trip
            .Include(r => r.Items)       // List Item Belanja
            .Include(r => r.ApprovalLogs) // History Approval
                .ThenInclude(log => log.User) // Nama Approver (Manager/Finance)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<Reimbursement?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Reimbursements
            .Include(r => r.User)
            .Include(r => r.Category)
            .Include(r => r.Trip)
            .Include(r => r.Items)
            .Include(r => r.ApprovalLogs)
                .ThenInclude(log => log.User)
            // NO AsNoTracking() -> Connected Entity
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Reimbursement>> GetPendingForManagerAsync(Guid managerId, CancellationToken cancellationToken)
    {
        // Logic Query:
        // 1. Ambil Reimbursement yang Global Statusnya 'Pending'
        // 2. Filter User yang ManagerId-nya adalah requester (bawahan saya)
        // 3. Filter Log Terakhir harus 'Submitted'

        return await _context.Reimbursements
            .Include(r => r.User)
            .Include(r => r.Category)
            .Include(r => r.Trip)
            .Include(r => r.ApprovalLogs) // Kita butuh ini untuk sort
            .Where(r =>
                r.ReimbursementStatus == ReimbursementStatus.Pending && // Global masih Pending
                r.User!.ManagerId == managerId // Milik bawahan
            )
            // Filter Lanjutan (Log Terakhir)
            // Note: EF Core yang baru sudah cukup pintar menerjemahkan OrderByDescending().FirstOrDefault()
            .Where(r => r.ApprovalLogs
                .OrderByDescending(l => l.CreatedAt)
                .FirstOrDefault()!.ApprovalLogStatus == ApprovalLogStatus.Submitted)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    // Tambahkan di dalam class ReimbursementRepository

    public async Task<IEnumerable<Reimbursement>> GetByUserIdWithDetailsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Reimbursements
            .Include(r => r.User)
            .Include(r => r.Category)
            .Include(r => r.Trip)
            .Include(r => r.Items)
            .Include(r => r.ApprovalLogs)
                .ThenInclude(l => l.User)
            .Where(r => r.UserId == userId)
            .Where(r =>
                // CASE A: Reimbursement Biasa (Bukan Trip) -> Tampilkan
                r.TripId == null
                ||
                // CASE B: Reimbursement Trip -> Hanya jika Trip sudah Ongoing atau Closed
                (r.Trip != null && (
                    r.Trip.TripStatus == TripStatus.Ongoing ||
                    r.Trip.TripStatus == TripStatus.Closed
                ))
            )
            .OrderByDescending(r => r.CreatedAt)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Reimbursement>> GetPendingForFinanceAsync(CancellationToken cancellationToken)
    {
        return await _context.Reimbursements
            .Include(r => r.User)
                .ThenInclude(u => u!.Account)
            .Include(r => r.Category)
            .Include(r => r.Trip)
            .Include(r => r.Items)
            .Include(r => r.ApprovalLogs)
                .ThenInclude(l => l.User)
            .Where(r =>
                r.ReimbursementStatus == ReimbursementStatus.Pending
            )
            .Where(r => r.ApprovalLogs
                .OrderByDescending(l => l.CreatedAt)
                .FirstOrDefault()!.ApprovalLogStatus == ApprovalLogStatus.ManagerApproved)
            .OrderBy(r => r.CreatedAt)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
