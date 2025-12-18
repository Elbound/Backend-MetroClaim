using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MetroClaim.Api.Services.Interfaces;
using MetroClaim.Api.Utilities;

namespace MetroClaim.Api.Controllers;

[ApiController]
[Route("api/finance")]
[Authorize(Roles = "Finance")]
public class FinanceController : ControllerBase
{
    private readonly IFinanceService _financeService;

    public FinanceController(IFinanceService financeService)
    {
        _financeService = financeService;
    }

    [HttpPost("process-salary")]
    public async Task<IActionResult> ProcessSalary(CancellationToken cancellationToken)
    {
        await _financeService.ProcessMonthlySalaryAsync(cancellationToken);
        return Ok(new ApiResponse<object>("Monthly salary processed successfully. Reimbursements reset."));
    }
}
