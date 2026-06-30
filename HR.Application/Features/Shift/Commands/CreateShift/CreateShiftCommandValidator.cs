using FluentValidation;
using HR.Application.Features.Shift.Commands.Shared;

namespace HR.Application.Features.Shift.Commands.CreateShift
{
    public class CreateShiftCommandValidator : ShiftCommandValidatorBase<CreateShiftCommand>
    {
        public CreateShiftCommandValidator()
        {

        }
    }
}
