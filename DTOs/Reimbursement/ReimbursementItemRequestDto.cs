using System;

namespace MetroClaim.Api.DTOs.Reimbursement;

public record ReimbursementItemRequestDto(
    decimal Amount,
    DateTime DateOfExpense,
    string? Receipt
);