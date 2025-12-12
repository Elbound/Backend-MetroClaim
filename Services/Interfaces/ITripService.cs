using MetroClaim.Api.DTOs.Trip;
using MetroClaim.Api.Models;

namespace MetroClaim.Api.Services.Interfaces;

public interface ITripService
{    
    Task<TripDetailDto> GetTripByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TripDetailDto>> GetTripsCreatedByMeAsync(CancellationToken cancellationToken);
    Task<IEnumerable<TripDetailDto>> GetMyAssignedTripsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<TripDetailDto>> GetTripsForFinanceAsync(CancellationToken cancellationToken);

    // WRITE
    Task<TripDetailDto> CreateTripAsync(CreateTripRequestDto requestDto, CancellationToken cancellationToken);
    Task UpdateTripAsync(Guid id, UpdateTripRequestDto requestDto, CancellationToken cancellationToken);
    Task CancelTripAsync(Guid id, CancellationToken cancellationToken);

    // WORKFLOW
    Task ReviewTripByFinanceAsync(Guid id, FinanceReviewTripDto requestDto, CancellationToken cancellationToken);
    Task PublishTripAsync(Guid id, CancellationToken cancellationToken);

}
