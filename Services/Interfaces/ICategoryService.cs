using MetroClaim.Api.DTOs.Category;

namespace MetroClaim.Api.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync(CancellationToken cancellationToken);
    Task<CategoryResponseDto> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken);

    Task CreateCategoryAsync(CategoryRequestDto request, CancellationToken cancellationToken);
    Task UpdateCategoryAsync(Guid id, CategoryRequestDto request, CancellationToken cancellationToken);
    Task DeleteCategoryAsync(Guid id, CancellationToken cancellationToken);
}

