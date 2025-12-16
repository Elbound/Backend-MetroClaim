using FluentValidation;
using MetroClaim.Api.DTOs.Trip;

namespace MetroClaim.Api.Utilities.Validation.Trip;

public class CreateTripRequestValidator : AbstractValidator<CreateTripRequestDto>
{
    public CreateTripRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required");

        RuleFor(x => x.Destination)
            .NotEmpty().WithMessage("Destination is required");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required")
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Start date cannot be in the past");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required")
            .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End date must be after or equal to start date");

        RuleFor(x => x.ParticipantIds)
            .NotEmpty().WithMessage("At least one participant is required");
    }
}
