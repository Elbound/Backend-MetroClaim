using MetroClaim.Api.Models;

namespace MetroClaim.Api.Repositories.Interfaces;

public interface ITripRepository : IRepository<Trip>
{

    // Ambil 1 trip dengan detail lengkap
    Task<Trip?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken);
    
    // Ambil trip berdasarkan Creator (Manager)
    Task<IEnumerable<Trip>> GetByManagerIdAsync(Guid managerId, CancellationToken cancellationToken);
    
    // Ambil trip di mana User menjadi peserta (via Reimbursement)
    Task<IEnumerable<Trip>> GetByParticipantIdAsync(Guid userId, CancellationToken cancellationToken);
    
    // Ambil trip untuk Finance (Submitted / FinanceApproved)
    Task<IEnumerable<Trip>> GetForFinanceAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Trip>> GetHistoryForFinanceAsync(CancellationToken cancellationToken);
    
    Task<Trip?> GetByIdWithParticipantsAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Guid>> GetConflictingUserIdsAsync(IEnumerable<Guid> participantIds, DateTime startDate, DateTime endDate, Guid? excludeTripId, CancellationToken cancellationToken);

}
