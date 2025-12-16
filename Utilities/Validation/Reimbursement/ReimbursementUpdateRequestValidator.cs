using FluentValidation;
using MetroClaim.Api.DTOs.Reimbursement;

namespace MetroClaim.Api.Utilities.Validation.Reimbursement;

public class ReimbursementUpdateRequestValidator : AbstractValidator<ReimbursementUpdateRequestDto>
{
    public ReimbursementUpdateRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters");

        RuleFor(x => x.Description)
            .NotNull().WithMessage("Description is required");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("At least one item is required")
            .Must(items => items != null && items.Any()).WithMessage("At least one item is required");

        RuleForEach(x => x.Items).SetValidator(new ReimbursementItemRequestValidator());
    }
}
