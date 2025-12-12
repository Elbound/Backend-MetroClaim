using System;

namespace MetroClaim.Api.DTOs.Reimbursement;

public record ReimbursementCreateRequestDto(
    string Title,
    string Description,
    Guid CategoryId,
    Guid? TripId,
    // decimal TotalAmount,
    List<ReimbursementItemRequestDto> Items
);