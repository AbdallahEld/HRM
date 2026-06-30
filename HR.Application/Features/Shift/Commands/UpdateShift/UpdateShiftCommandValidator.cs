using FluentValidation;
using HR.Application.Features.Shift.Commands.Shared;

namespace HR.Application.Features.Shift.Commands.UpdateShift
{
    public class UpdateShiftCommandValidator : ShiftCommandValidatorBase<UpdateShiftCommand>
    {
        public UpdateShiftCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Valid Shift ID is required for update.");
        }
    }
}
