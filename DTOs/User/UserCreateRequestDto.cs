namespace MetroClaim.Api.DTOs.User;

public record UserCreateRequestDto
(
    string EmployeeId,
    string FullName,
    decimal Salary,
    decimal DueReimbursement,
    string BankAccountNumber,
    Guid? ManagerId
);
