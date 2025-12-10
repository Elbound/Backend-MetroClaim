using MetroClaim.Api.Models;

namespace MetroClaim.Api.Repositories.Interfaces;

public interface ITripRepository : IRepository<Trip>
{

    Task<IEnumerable<Trip>> GetAllManagerSubmitStatusAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Trip>> GetAllTripByIdAsync(Guid id, CancellationToken cancellationToken);
    

}
