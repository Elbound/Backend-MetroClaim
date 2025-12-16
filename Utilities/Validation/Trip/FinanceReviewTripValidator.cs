using FluentValidation;
using MetroClaim.Api.DTOs.Trip;

namespace MetroClaim.Api.Utilities.Validation.Trip;

public class FinanceReviewTripValidator : AbstractValidator<FinanceReviewTripDto>
{
    public FinanceReviewTripValidator()
    {
        RuleFor(x => x.AllocatedCost)
            .GreaterThanOrEqualTo(0).WithMessage("Allocated cost must be non-negative")
            .When(x => x.IsApproved);
            
        RuleFor(x => x.RejectionReason)
            .NotEmpty().WithMessage("Rejection reason is required")
            .When(x => !x.IsApproved);
    }
}
