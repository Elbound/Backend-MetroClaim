using MetroClaim.Api.DTOs.Trip;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories;
using MetroClaim.Api.Repositories.Interfaces;
using MetroClaim.Api.Services.Interfaces;

namespace MetroClaim.Api.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TripService(ITripRepository tripRepository, IUnitOfWork unitOfWork)
    {
        _tripRepository = tripRepository;
        _unitOfWork = unitOfWork;
    }



    public async Task CancelTripAsync(Guid id, CancellationToken cancellationToken)
    {
        var trip = await _tripRepository.GetByIdAsync(id, cancellationToken);

        trip.TripStatus = TripStatus.Canceled;

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _tripRepository.UpdateAsync(trip);
        },cancellationToken);
    }

    public async Task CreateTripAsync(TripCreateRequestDto request, CancellationToken cancellationToken)
    {
        var newTrip = new Trip
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Empty,
            Title = request.Title,
            Description = request.Description,
            Destination = request.Destination,
            TripStatus = TripStatus.ManagerSubmited
        };
        var userLists = request.UserIds;
        
    }

    public async Task DeleteTripAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TripResponseDTO>> GetAllSubmitedTripAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TripResponseDTO>> GetAllTripAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TripResponseDTO>> GetAllTripCreatedById(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<TripResponseDTO> GetTripByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateTripCostAsync(Guid id, decimal cost, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateTripStatusAsync(Guid id, TripStatus status, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

}
