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
    DateTime UpdatedAt
);

/*
    public Guid Id { get; set; }
    public string? EmployeeId { get; set; }
    public string? FullName { get; set; }
    public decimal Salary { get; set; }
    public decimal DueReimbursement { get; set; }
    public string? BankAccountNumber { get; set; }
    public Guid? ManagerId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
*/