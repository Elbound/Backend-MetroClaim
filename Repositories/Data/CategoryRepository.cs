using MetroClaim.Api.Data;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories.Interfaces;

namespace MetroClaim.Api.Repositories.Data;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(MetroClaimApiDbContext context) : base(context)
    {
    }
}
