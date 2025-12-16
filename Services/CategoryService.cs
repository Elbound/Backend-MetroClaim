using MetroClaim.Api.DTOs.Category;
using MetroClaim.Api.Models;
using MetroClaim.Api.Repositories;
using MetroClaim.Api.Repositories.Interfaces;
using MetroClaim.Api.Services.Interfaces;

namespace MetroClaim.Api.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }



    public async Task CreateCategoryAsync(CategoryRequestDto request, CancellationToken cancellationToken)
    {
        var newCategory = new Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Limit = request.Limit,
            CreatedAt = DateTime.Now
        };

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _categoryRepository.CreateAsync(newCategory,cancellationToken);
        },cancellationToken);
    }

    public async Task DeleteCategoryAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _categoryRepository.DeleteAsync(category);
        },cancellationToken);

    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync(CancellationToken cancellationToken)
    {
        var getAllCategory = await _categoryRepository.GetAllAsync(cancellationToken);

        var allCategory = getAllCategory
            .Where(c => c.Id != Guid.Parse("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"))
            .Select(c=>new CategoryResponseDto(
            c.Id,
            c.Name,
            c.Limit
        ));

        return allCategory;
    }

    public async Task<CategoryResponseDto> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var getCategory = await _categoryRepository.GetByIdAsync(id, cancellationToken);

        var category = new CategoryResponseDto(
            getCategory.Id,
            getCategory.Name,
            getCategory.Limit
        );

        return category;
    }

    public async Task UpdateCategoryAsync(Guid id, CategoryRequestDto request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);

        category.Name = request.Name;
        category.Limit = request.Limit;
        category.UpdatedAt = DateTime.Now;

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _categoryRepository.UpdateAsync(category);
        } ,cancellationToken);
    }

}
