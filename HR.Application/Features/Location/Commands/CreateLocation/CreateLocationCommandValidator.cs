using FluentValidation;
using HR.Application.Features.Location.Commands.Shared;

namespace HR.Application.Features.Location.Commands.CreateLocation
{
    public class CreateLocationCommandValidator : LocationCommandValidatorBase<CreateLocationCommand>
    {
        public CreateLocationCommandValidator()
        {
            
        }
    }
}
