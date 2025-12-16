using FluentValidation;
using MetroClaim.Api.DTOs.Auth;

namespace MetroClaim.Api.Utilities.Validation.Auth;

public class AuthLoginRequestValidator : AbstractValidator<AuthLoginRequestDto>
{
    public AuthLoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}
