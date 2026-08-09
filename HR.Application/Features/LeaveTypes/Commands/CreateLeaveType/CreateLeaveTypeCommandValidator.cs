using FluentValidation;
using HR.Application.Features.LeaveTypes.Commands.Shared;

namespace HR.Application.Features.LeaveTypes.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandValidator : LeaveTypeCommandValidatorBase<CreateLeaveTypeCommand>
    {
        public CreateLeaveTypeCommandValidator()
        {
            
        }

    }
}
