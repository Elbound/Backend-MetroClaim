using MetroClaim.Api.DTOs.Reimbursement;
using MetroClaim.Api.Models;

namespace MetroClaim.Api.Services.Interfaces;

public interface IReimbursementService
{
    Task<IEnumerable<ReimbursemenGetResponseDto>> GetAllReimbursementsAsync(CancellationToken cancellationToken);

    Task<ReimbursementDetailDto> GetReimbursementByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<ReimbursementDetailDto>> GetMyReimbursementsAsync(CancellationToken cancellationToken);

    Task<IEnumerable<ReimbursementDetailDto>> GetSubordinateReimbursementsAsync(CancellationToken cancellationToken);

    Task<IEnumerable<ReimbursementDetailDto>> GetForFinanceAsync(CancellationToken cancellationToken);

    Task<ReimbursementDetailDto> CreateReimbursementAsync(ReimbursementCreateRequestDto requestDto, CancellationToken cancellationToken);

    Task UpdateReimbursementAsync(Guid id, ReimbursementUpdateRequestDto requestDto, CancellationToken cancellationToken);

    Task DeleteReimbursementAsync(Guid id, CancellationToken cancellationToken);
}

