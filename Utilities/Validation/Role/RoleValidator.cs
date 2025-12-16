using FluentValidation;
using MetroClaim.Api.DTOs.Role;

namespace MetroClaim.Api.Utilities.Validation.Role;

public class RoleValidator : AbstractValidator<RoleDTO>
{
    public RoleValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
