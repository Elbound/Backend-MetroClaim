namespace MetroClaim.Api.DTOs.ApprovalLog;

public record ApprovalLogDto(
    Guid Id,
    string ApproverName,
    string Action,
    string? Comment,
    DateTime CreatedAt
);