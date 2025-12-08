namespace MetroClaim.Api.Models;

public class Category
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public decimal Limit { get; set; }
    public Period Period { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<UserLimit> UserLimits { get; set; } = new List<UserLimit>();
    public virtual ICollection<Reimbursement> Reimbursements { get; set; } = new List<Reimbursement>();
}