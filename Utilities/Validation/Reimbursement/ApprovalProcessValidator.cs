using FluentValidation;
using MetroClaim.Api.DTOs.Reimbursement;

namespace MetroClaim.Api.Utilities.Validation.Reimbursement;

public class ApprovalProcessValidator : AbstractValidator<ApprovalProcessDto>
{
    public ApprovalProcessValidator()
    {
        RuleFor(x => x.Action)
            .IsInEnum().WithMessage("Invalid approval action");

        RuleFor(x => x.Comment)
            .NotEmpty()
            .When(x => x.Action == ApprovalAction.Reject || x.Action == ApprovalAction.Revise)
            .WithMessage("Comment is required when rejecting or asking for revision.");
    }
}
