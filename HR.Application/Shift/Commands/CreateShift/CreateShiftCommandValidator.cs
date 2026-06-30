using FluentValidation;
using HR.Application.Shift.Commands.Shared;

namespace HR.Application.Shift.Commands.CreateShift
{
    public class CreateShiftCommandValidator : ShiftCommandValidatorBase<CreateShiftCommand>
    {
        public CreateShiftCommandValidator()
        {

        }
    }
}
