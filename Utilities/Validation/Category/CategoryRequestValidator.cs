using FluentValidation;
using MetroClaim.Api.DTOs.Category;

namespace MetroClaim.Api.Utilities.Validation.Category;

public class CategoryRequestValidator : AbstractValidator<CategoryRequestDto>
{
    public CategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required")
            .MaximumLength(100).WithMessage("Category name must not exceed 100 characters");

        RuleFor(x => x.Limit)
            .GreaterThan(0).WithMessage("Limit must be greater than 0");
    }
}
