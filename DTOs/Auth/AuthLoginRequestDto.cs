namespace MetroClaim.Api.DTOs.Auth;

public record AuthLoginRequestDto
(
    string Email,
    string Password
);