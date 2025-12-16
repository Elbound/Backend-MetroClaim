using FluentValidation;
using MetroClaim.Api.DTOs.UserLimit;

namespace MetroClaim.Api.Utilities.Validation.UserLimit;

public class CreateUserLimitRequestValidator : AbstractValidator<CreateUserLimitRequestDto>
{
    public CreateUserLimitRequestValidator()
    {
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}
