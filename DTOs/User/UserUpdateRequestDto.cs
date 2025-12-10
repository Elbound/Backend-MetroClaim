namespace MetroClaim.Api.DTOs.User;

public record UserUpdateRequestDto
(
    string EmployeeId,
    string FullName,
    decimal Salary,
    decimal DueReimbursement,
    string BankAccountNumber,
    Guid? ManagerId,
    List<Guid> RoleIds
);