using System;
using MetroClaim.Api.Data;
using MetroClaim.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MetroClaim.Api.Repositories.Data;


public class Repository<T> : IRepository<T> where T : class
{
    private readonly MetroClaimApiDbContext _context;

    public Repository(MetroClaimApiDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(T t, CancellationToken cancellationToken)
    {
        await _context.Set<T>().AddAsync(t, cancellationToken);
    }

    public async Task DeleteAsync(T t)
    {
        _context.Set<T>().Remove(t);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().FindAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(T t)
    {
        _context.Set<T>().Update(t);
    }

    public async Task CreateRangeAsync(IEnumerable<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }
}
