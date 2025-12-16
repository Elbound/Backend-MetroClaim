using FluentValidation;
using MetroClaim.Api.DTOs.User;

namespace MetroClaim.Api.Utilities.Validation.User;

public class UserCreateRequestValidator : AbstractValidator<UserCreateRequestDto>
{
    public UserCreateRequestValidator()
    {
        RuleFor(x => x.EmployeeId).NotEmpty();
        RuleFor(x => x.FullName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.Salary).GreaterThanOrEqualTo(0);
        RuleFor(x => x.RoleIds).NotEmpty();
    }
}
