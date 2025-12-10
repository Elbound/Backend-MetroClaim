using MetroClaim.Api.Models;

namespace MetroClaim.Api.DTOs.Trip;

public record TripResponseDTO
(
    Guid Id,
    string Title,
    string Description,
    string Destination,
    decimal cost,
    TripStatus TripStatus

);
