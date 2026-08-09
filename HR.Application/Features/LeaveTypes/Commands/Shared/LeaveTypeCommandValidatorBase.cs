using FluentValidation;

namespace HR.Application.Features.LeaveTypes.Commands.Shared
{
    public class LeaveTypeCommandValidatorBase<T> : AbstractValidator<T> where T : ILeaveTypeCommand
    {
        public LeaveTypeCommandValidatorBase()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.MaxDaysPerYear)
                .InclusiveBetween(1, 365).WithMessage("Max days per year must be between 1 and 365.");

        }
    }
}
