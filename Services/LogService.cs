using MetroClaim.Api.DTOs.Log;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories;
using MetroClaim.Api.Repositories.Interfaces;
using MetroClaim.Api.Services.Interfaces;
using Org.BouncyCastle.Ocsp;

namespace MetroClaim.Api.Services;

public class LogService : ILogService
{
    private readonly IApprovalLogRepository  _logRepository;
    private readonly IUnitOfWork _unitOfWork;
    public LogService(IApprovalLogRepository logRepository, IUnitOfWork unitOfWork)
    {
        _logRepository = logRepository;
        _unitOfWork= unitOfWork;
    }


    public async Task CreateReimbursementLogAsync(LogRequestDto request, CancellationToken cancellationToken)
    {
        var log = new ApprovalLog
        {
            Id = Guid.NewGuid(),
            ReimbursementId = request.ReimbursementId,
            UserId = request.UserId,
            ApprovalLogStatus = request.Status,
            Comment = request.Comment,
            CreatedAt = DateTime.Now
        };

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _logRepository.CreateAsync(log,cancellationToken);
        },cancellationToken);
    }

    public async Task<IEnumerable<LogResponseDto>> GetReimbursementLogAsync(Guid id, CancellationToken cancellationToken)
    {
        var allLog = await _logRepository.GetAllByReimbursementId(id, cancellationToken);

        var logs = allLog.Select(al => new LogResponseDto(
            al.Id,
            al.ReimbursementId,
            al.UserId,
            al.ApprovalLogStatus,
            al.Comment,
            al.CreatedAt
        ));

        return logs;
    }

}
