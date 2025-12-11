namespace MetroClaim.Api.DTOs.Reimbursement;

public record ReimbursemenGetResponseDto(
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
    DateTime UpdatedAt
);