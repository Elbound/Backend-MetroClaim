namespace MetroClaim.Api.DTOs.Category;

public record CategoryResponseDto
(
    Guid Id,
    string Name,
    decimal Limit
);