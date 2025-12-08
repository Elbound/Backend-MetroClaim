namespace MetroClaim.Api.Models;

public class Reimbursement
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid? TripId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal TotalAmount { get; set; }
    public ReimbursementStatus ReimbursementStatus { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual User? User { get; set; }
    public virtual Category? Category { get; set; }
    public virtual Trip? Trip { get; set; }
    public virtual ICollection<ReimbursementItem> Items { get; set; } = new List<ReimbursementItem>();
    public virtual ICollection<ApprovalLog> ApprovalLogs { get; set; } = new List<ApprovalLog>();
}
