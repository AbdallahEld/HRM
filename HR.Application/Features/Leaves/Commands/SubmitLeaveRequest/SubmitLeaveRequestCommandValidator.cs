using FluentValidation;

namespace HR.Application.Features.Leaves.Commands.SubmitLeaveRequest
{
    public class SubmitLeaveRequestCommandValidator : AbstractValidator<SubmitLeaveRequestCommand>
    {
        public SubmitLeaveRequestCommandValidator()
        {
            RuleFor(l => l.StartDate)
                .NotEmpty().WithMessage("Start date is required.")
                .LessThanOrEqualTo(l => l.EndDate).WithMessage("Start date must be before or equal to end date.");

            RuleFor(l => l.EndDate)
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThanOrEqualTo(l => l.StartDate).WithMessage("End date must be after or equal to start date.");
        }
    }
}
