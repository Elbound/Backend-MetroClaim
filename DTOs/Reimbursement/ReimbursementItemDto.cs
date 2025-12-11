namespace MetroClaim.Api.DTOs.Reimbursement;

public record ReimbursementItemDto(
    Guid Id,
    decimal Amount,
    DateTime DateOfExpense,
    string? Receipt
);