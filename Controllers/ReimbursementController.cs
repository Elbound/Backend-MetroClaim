using MetroClaim.Api.DTOs.Reimbursement;
using MetroClaim.Api.Models;
using MetroClaim.Api.Services.Interfaces;
using MetroClaim.Api.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace MetroClaim.Api.Controllers;

[ApiController]
[Route("api/reimbursement")]
public class ReimbursementController : ControllerBase
{
    private readonly IReimbursementService _reimbursementService;

    public ReimbursementController(IReimbursementService reimbursementService)
    {
        _reimbursementService = reimbursementService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReimbursement(ReimbursementCreateRequestDto requestDto, CancellationToken cancellationToken)
    {
        var reimbursement = await _reimbursementService.CreateReimbursementAsync(requestDto, cancellationToken);
        return Ok(new ApiResponse<ReimbursementDetailDto>(reimbursement));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReimbursement(CancellationToken cancellationToken)
    {
        var reimbursements = await _reimbursementService.GetAllReimbursementsAsync(cancellationToken);
        return Ok(new ApiResponse<IEnumerable<ReimbursemenGetResponseDto>>(reimbursements));
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetReimbursementById(Guid id, CancellationToken cancellationToken)
    {
        var reimbursement = await _reimbursementService.GetReimbursementByIdAsync(id, cancellationToken);
        return Ok(new ApiResponse<ReimbursementDetailDto>(reimbursement));
    }

    [HttpGet("manager")]
    public async Task<IActionResult> GetAllSubordinateReimbursement(CancellationToken cancellationToken)
    {
        var reimbursements = await _reimbursementService.GetSubordinateReimbursementsAsync(cancellationToken);
        return Ok(new ApiResponse<IEnumerable<ReimbursementDetailDto>>(reimbursements));
    }

    [HttpGet("finance")]
    public async Task<IActionResult> GetManagerApprovedReimbursement(CancellationToken cancellationToken)
    {
        var reimbursements = await _reimbursementService.GetForFinanceAsync(cancellationToken);
        return Ok(new ApiResponse<IEnumerable<ReimbursementDetailDto>>(reimbursements));
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyReimbursement(CancellationToken cancellationToken)
    {
        var reimbursements = await _reimbursementService.GetMyReimbursementsAsync(cancellationToken);
        return Ok(new ApiResponse<IEnumerable<ReimbursementDetailDto>>(reimbursements));
    }
}
