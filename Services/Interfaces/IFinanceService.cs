namespace MetroClaim.Api.Services.Interfaces;

public interface IFinanceService
{
    Task ProcessMonthlySalaryAsync(CancellationToken cancellationToken);
}
