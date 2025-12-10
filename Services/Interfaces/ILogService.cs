using MetroClaim.Api.DTOs.Log;

namespace MetroClaim.Api.Services.Interfaces;

public interface ILogService
{
    Task CreateReimbursementLogAsync(LogRequestDto request, CancellationToken cancellationToken);
    Task<IEnumerable<LogResponseDto>> GetReimbursementLogAsync(Guid id, CancellationToken cancellationToken);
}
