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
            .Include(r => r.Items)
            .Include(r => r.ApprovalLogs)
                .ThenInclude(l => l.User)
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

    public async Task<IEnumerable<Reimbursement>> GetHistoryForManagerAsync(Guid managerId, CancellationToken cancellationToken)
    {
        return await _context.Reimbursements
            .Include(r => r.User)
            .Include(r => r.Category)
            .Include(r => r.Trip)
            .Include(r => r.Trip)
            // .Include(r => r.Items) intentionally removed
            .Include(r => r.ApprovalLogs)
                .ThenInclude(l => l.User)
            .Where(r => r.User!.ManagerId == managerId)
            // .Where(r =>
            //     r.ReimbursementStatus != ReimbursementStatus.Pending ||
            //     (
            //         r.ReimbursementStatus == ReimbursementStatus.Pending &&
            //         r.ApprovalLogs.OrderByDescending(l => l.CreatedAt).FirstOrDefault()!.ApprovalLogStatus != ApprovalLogStatus.Submitted
            //     )
            // )
            .OrderByDescending(r => r.UpdatedAt)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Reimbursement>> GetByUserIdWithDetailsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Reimbursements
            .Include(r => r.User)
            .Include(r => r.Category)
            .Include(r => r.Trip)
            // .Include(r => r.Items)
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

    public async Task<(IEnumerable<Reimbursement> Items, int TotalCount)> GetByUserIdPagedAsync(
        Guid userId, 
        int page, 
        int pageSize, 
        string? search, 
        string? status, 
        CancellationToken cancellationToken)
    {
        var query = _context.Reimbursements
            .Include(r => r.User)
            .Include(r => r.Category)
            .Include(r => r.Trip)
            // .Include(r => r.Items) // Optional: exclude for list view performance
            .Include(r => r.ApprovalLogs)
                .ThenInclude(l => l.User)
            .Where(r => r.UserId == userId)
            // Existing Trip Visibility Logic
            .Where(r =>
                r.TripId == null ||
                (r.Trip != null && (
                    r.Trip.TripStatus == TripStatus.Ongoing ||
                    r.Trip.TripStatus == TripStatus.Closed
                ))
            );

        // 1. Search Filter (Title)
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(r => r.Title != null && r.Title.Contains(search));
        }

        // 2. Status Filter (Based on Last Log Action)
        if (!string.IsNullOrWhiteSpace(status))
        {
            // Normalize status string
            var statusLower = status.Trim().ToLower();

            if (statusLower == "ongoing") // "Submitted"
            {
                query = query.Where(r => r.ApprovalLogs
                    .OrderByDescending(l => l.CreatedAt)
                    .FirstOrDefault()!.ApprovalLogStatus == ApprovalLogStatus.Submitted);
            }
            else if (statusLower == "draft") // "Drafted"
            {
                query = query.Where(r => r.ApprovalLogs
                    .OrderByDescending(l => l.CreatedAt)
                    .FirstOrDefault()!.ApprovalLogStatus == ApprovalLogStatus.Drafted);
            }
            else if (statusLower == "revision") // "ManagerRevision"
            {
                query = query.Where(r => r.ApprovalLogs
                    .OrderByDescending(l => l.CreatedAt)
                    .FirstOrDefault()!.ApprovalLogStatus == ApprovalLogStatus.ManagerRevision);
            }
            else if (statusLower == "rejected") // ManagerRejected OR FinanceRejected
            {
                query = query.Where(r => 
                    r.ApprovalLogs.OrderByDescending(l => l.CreatedAt).FirstOrDefault()!.ApprovalLogStatus == ApprovalLogStatus.ManagerRejected ||
                    r.ApprovalLogs.OrderByDescending(l => l.CreatedAt).FirstOrDefault()!.ApprovalLogStatus == ApprovalLogStatus.FinanceRejected
                );
            }
            else if (Enum.TryParse<ApprovalLogStatus>(status, true, out var logStatus))
            {
                // Fallback for direct matches like "ManagerApproved", "FinanceApproved"
                query = query.Where(r => r.ApprovalLogs
                    .OrderByDescending(l => l.CreatedAt)
                    .FirstOrDefault()!.ApprovalLogStatus == logStatus);
            }
        }

        // 3. Count Total (Filtered)
        var totalCount = await query.CountAsync(cancellationToken);

        // 4. Pagination
        var items = await query
            .OrderByDescending(r => r.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return (items, totalCount);
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

    public async Task<IEnumerable<Reimbursement>> GetHistoryForFinanceAsync(CancellationToken cancellationToken)
    {
        return await _context.Reimbursements
           .Include(r => r.User)
           .Include(r => r.Category)
           .Include(r => r.Trip)
           // No Items for history view
           .Include(r => r.ApprovalLogs)
               .ThenInclude(l => l.User)
           // Finance sees everything? Or just what reached them?
           // "Segala kondisi" -> All.
           .OrderByDescending(r => r.CreatedAt)
           .AsNoTracking()
           .ToListAsync(cancellationToken);
    }
}
