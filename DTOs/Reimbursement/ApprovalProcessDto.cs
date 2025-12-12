namespace MetroClaim.Api.DTOs.Reimbursement;

public enum ApprovalAction
{
    Approve,
    Reject,
    Revise // Khusus Manager
}

public record ApprovalProcessDto(
    ApprovalAction Action,
    string? Comment // Wajib diisi jika Reject/Revise
);