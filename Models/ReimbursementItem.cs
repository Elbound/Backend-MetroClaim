namespace MetroClaim.Api.Models;

public class ReimbursementItem
{
    public Guid Id { get; set; }
    public Guid ReimbursementId { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateOfExpense { get; set; }
    public string? Receipt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual Reimbursement? Reimbursement { get; set; }
}
