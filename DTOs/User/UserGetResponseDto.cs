namespace MetroClaim.Api.DTOs.User;

public record class UserGetResponseDto
(
    Guid Id,
    string? EmployeeId,
    string? FullName,
    decimal Salary,
    decimal DueReimbursement,
    string? BankAccountNumber,
    Guid? ManagerId,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string? Email,
    List<string> Roles
);