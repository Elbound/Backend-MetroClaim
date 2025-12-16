using FluentValidation;
using MetroClaim.Api.DTOs.Reimbursement;

namespace MetroClaim.Api.Utilities.Validation.Reimbursement;

public class ReimbursementItemRequestValidator : AbstractValidator<ReimbursementItemRequestDto>
{
    public ReimbursementItemRequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");

        RuleFor(x => x.DateOfExpense)
            .NotEmpty().WithMessage("Date of expense is required")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Date of expense cannot be in the future");
    }
}
