using System;
using MetroClaim.Api.DTOs.ApprovalLog;

namespace MetroClaim.Api.DTOs.Reimbursement;

public record ReimbursementDetailDto(
    Guid Id,
    string UserEmployeeId, 
    string UserFullName,
    string CategoryName,
    string? TripTitle,
    string Title,
    string Description,
    decimal TotalAmount,
    string Status,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<ReimbursementItemDto> Items,
    List<ApprovalLogDto> Logs
);