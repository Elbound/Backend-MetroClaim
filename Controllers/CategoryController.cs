using MetroClaim.Api.DTOs.Category;
using MetroClaim.Api.Models;
using MetroClaim.Api.Services.Interfaces;
using MetroClaim.Api.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace MetroClaim.Api.Controllers;

[ApiController]
[Route("api/category")]
public class CategoryController: ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategory(CancellationToken cancellationToken)
    {
        var allCategory = await _categoryService.GetAllCategoriesAsync(cancellationToken);
        return Ok(new ApiResponse<IEnumerable<CategoryResponseDto>>(allCategory));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id, cancellationToken);
        return Ok(new ApiResponse<CategoryResponseDto>(category));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CategoryRequestDto request, CancellationToken cancellationToken)
    {
        await _categoryService.CreateCategoryAsync(request, cancellationToken);
        return Ok(new ApiResponse<object>("Category Created"));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory(Guid id, CategoryRequestDto request, CancellationToken cancellationToken)
    {
        await _categoryService.UpdateCategoryAsync(id, request, cancellationToken);
        return Ok(new ApiResponse<object>("Category Updated"));
    }

    [HttpPut]
    public async Task<IActionResult> DeleteCategory(Guid id, CategoryRequestDto request, CancellationToken cancellationToken)
    {
        await _categoryService.DeleteCategoryAsync(id, cancellationToken);
        return Ok(new ApiResponse<object>("Category Deleted"));
    }
}
