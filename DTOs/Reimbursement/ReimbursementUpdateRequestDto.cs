using System;

namespace MetroClaim.Api.DTOs.Reimbursement;

public record ReimbursementUpdateRequestDto(
    string Title,
    string Description,
    Guid? CategoryId,
    // Guid? TripId,
    List<ReimbursementItemRequestDto> Items 
);