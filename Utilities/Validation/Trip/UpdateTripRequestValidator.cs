using FluentValidation;
using MetroClaim.Api.DTOs.Trip;

namespace MetroClaim.Api.Utilities.Validation.Trip;

public class UpdateTripRequestValidator : AbstractValidator<UpdateTripRequestDto>
{
    public UpdateTripRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required");

        RuleFor(x => x.Destination)
            .NotEmpty().WithMessage("Destination is required");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required")
            .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End date must be after or equal to start date");
            
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category is required");

        RuleFor(x => x.ParticipantIds)
            .NotNull().WithMessage("Participant list cannot be null");
    }
}
