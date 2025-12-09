namespace MetroClaim.Api.DTOs.User;

public record UserUpdateRequestDto
(
    string EmployeeId, //model user
    string FullName, //model user
    decimal Salary, //model user
    decimal DueReimbursement, //model user
    string BankAccountNumber, //model user
    Guid? ManagerId //model user
);