using MetroClaim.Api.DTOs.Category;
using MetroClaim.Api.Services.Interfaces;
using MetroClaim.Api.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetroClaim.Api.Controllers;

[ApiController]
[Route("api/category")]
[Authorize]
public class CategoryController: ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [Authorize(Roles = "Employee,Admin")]
    public async Task<IActionResult> GetAllCategory(CancellationToken cancellationToken)
    {
        var allCategory = await _categoryService.GetAllCategoriesAsync(cancellationToken);
        return Ok(new ApiResponse<IEnumerable<CategoryResponseDto>>(allCategory));
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetCategoryById(Guid id, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id, cancellationToken);
        return Ok(new ApiResponse<CategoryResponseDto>(category));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateCategory(CategoryRequestDto request, CancellationToken cancellationToken)
    {
        await _categoryService.CreateCategoryAsync(request, cancellationToken);
        return Ok(new ApiResponse<object>("Category Created"));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCategory(Guid id, CategoryRequestDto request, CancellationToken cancellationToken)
    {
        await _categoryService.UpdateCategoryAsync(id, request, cancellationToken);
        return Ok(new ApiResponse<object>("Category Updated"));
    }

    // [HttpPut("{id}")]
    // public async Task<IActionResult> DeleteCategory(Guid id, CategoryRequestDto request, CancellationToken cancellationToken)
    // {
    //     await _categoryService.DeleteCategoryAsync(id, cancellationToken);
    //     return Ok(new ApiResponse<object>("Category Deleted"));
    // }
}
