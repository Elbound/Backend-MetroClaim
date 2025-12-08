using MetroClaim.Api.Data;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories.Interfaces;

namespace MetroClaim.Api.Repositories.Data;

public class TripRepository : Repository<Trip>, ITripRepository
{
    public TripRepository(MetroClaimApiDbContext context) : base(context)
    {
    }
}
