using MetroClaim.Api.Repositories.Interfaces;
using MetroClaim.Api.Services.Interfaces;

namespace MetroClaim.Api.Services;

public class FinanceService : IFinanceService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserLimitRepository _userLimitRepository;
    private readonly IUserContext _userContext;

    public FinanceService(
        IUserRepository userRepository, 
        IUserLimitRepository userLimitRepository,
        IUserContext userContext)
    {
        _userRepository = userRepository;
        _userLimitRepository = userLimitRepository;
        _userContext = userContext;
    }

    public async Task ProcessMonthlySalaryAsync(CancellationToken cancellationToken)
    {
        if (!_userContext.IsInRole("Finance"))
        {
            throw new UnauthorizedAccessException("Access denied. Finance role required.");
        }

        // 1. Reset Semua Due Reimbursement (Anggap sudah dibayar payroll)
        await _userRepository.ResetAllDueReimbursementsAsync(cancellationToken);

        // 2. Reset Limit Bulanan (Reset cycle baru)
        await _userLimitRepository.ResetAllLimitsAsync(cancellationToken);
    }
}
