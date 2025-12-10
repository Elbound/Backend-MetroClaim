using System.Data.Common;
using MetroClaim.Api.Data;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MetroClaim.Api.Repositories.Data;

public class TripRepository : Repository<Trip>, ITripRepository
{
    private readonly MetroClaimApiDbContext _context;
    public TripRepository(MetroClaimApiDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Trip>> GetAllManagerSubmitStatusAsync(CancellationToken cancellationToken)
    {
        return  _context.Set<Trip>().Where(x=> x.TripStatus == TripStatus.ManagerSubmited);
    }

    public async Task<IEnumerable<Trip>> GetAllTripByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Set<Trip>().Where(x=> x.UserId == id);
    }
}
