using FluentValidation;
using MetroClaim.Api.DTOs.User;

namespace MetroClaim.Api.Utilities.Validation.User;

public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequestDto>
{
    public UserUpdateRequestValidator()
    {
        RuleFor(x => x.EmployeeId).NotEmpty();
        RuleFor(x => x.FullName).NotEmpty();
        RuleFor(x => x.Salary).GreaterThanOrEqualTo(0);
        RuleFor(x => x.RoleIds).NotEmpty();
    }
}
