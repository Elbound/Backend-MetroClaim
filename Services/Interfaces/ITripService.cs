using MetroClaim.Api.DTOs.Trip;
using MetroClaim.Api.Models;

namespace MetroClaim.Api.Services.Interfaces;

public interface ITripService
{
    Task<IEnumerable<TripResponseDTO>> GetAllTripAsync(CancellationToken cancellationToken);
    Task<IEnumerable<TripResponseDTO>> GetAllSubmitedTripAsync(CancellationToken cancellationToken);
    Task<IEnumerable<TripResponseDTO>> GetAllTripCreatedById(Guid id, CancellationToken cancellationToken);
    Task<TripResponseDTO> GetTripByIdAsync(Guid id, CancellationToken cancellationToken);    
    
    Task CreateTripAsync(TripCreateRequestDto request, CancellationToken cancellationToken);
    Task UpdateTripCostAsync(Guid id, decimal cost, CancellationToken cancellationToken);
    Task CancelTripAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateTripStatusAsync(Guid id, TripStatus status, CancellationToken cancellationToken);
    Task DeleteTripAsync(Guid id, CancellationToken cancellationToken);

}
