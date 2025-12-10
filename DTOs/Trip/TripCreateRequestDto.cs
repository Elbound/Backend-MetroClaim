namespace MetroClaim.Api.DTOs.Trip;

public record TripCreateRequestDto
(
    string Title,
    string Description,
    string Destination,
    DateTime StartDate,
    DateTime EndDate,
    List<Guid> UserIds
);
