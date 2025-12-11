namespace MetroClaim.Api.DTOs.UserLimit;

public record UserLimitDto(
    Guid Id,
    Guid CategoryId,
    string CategoryName,
    decimal TotalLimit,
    decimal LimitUsed,
    decimal RemainingBalance
);