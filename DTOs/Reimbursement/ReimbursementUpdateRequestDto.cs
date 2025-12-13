using System;

namespace MetroClaim.Api.DTOs.Reimbursement;

public record ReimbursementUpdateRequestDto(
    string Title,
    string Description,
    // Guid? TripId,
    List<ReimbursementItemRequestDto> Items 
);