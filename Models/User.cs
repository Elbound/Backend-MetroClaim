namespace MetroClaim.Api.Models;

public class User
{
    public Guid Id { get; set; }
    public string? EmployeeId { get; set; }
    public string? FullName { get; set; }
    public decimal Salary { get; set; }
    public decimal DueReimbursement { get; set; }
    public string? BankAccountNumber { get; set; }
    public Guid? ManagerId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual User? Manager { get; set; }
    public virtual ICollection<User> Subordinates { get; set; } = new List<User>();
    public virtual Account? Account { get; set; }
    public virtual ICollection<Reimbursement> Reimbursements { get; set; } = new List<Reimbursement>();
    public virtual ICollection<UserLimit> UserLimits { get; set; } = new List<UserLimit>();
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public virtual ICollection<ApprovalLog> ApprovalLogs { get; set; } = new List<ApprovalLog>();
    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
