namespace MetroClaim.Api.Models;

public class ApprovalLog
{
    public Guid Id { get; set; }
    public Guid ReimbursementId { get; set; }
    public Guid UserId { get; set; }
    public ApprovalLogStatus ApprovalLogStatus { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual Reimbursement? Reimbursement { get; set; }
    public virtual User? User { get; set; }
}
