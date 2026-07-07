using FluentValidation;

namespace HR.Application.Features.Location.Commands.Shared
{
    public class LocationCommandValidatorBase <T> : AbstractValidator<T> where T : ILocationCommand
    {
        public LocationCommandValidatorBase()
        {
            RuleFor(l => l.Address)
                .MinimumLength(10).WithMessage("Address must be at least 10 characters long.");

            RuleFor(x => x.Lat)
            .InclusiveBetween(-90m, 90m)
            .WithMessage("Latitude must be between -90 and 90.");

            RuleFor(x => x.Long)
                .InclusiveBetween(-180m, 180m)
                .WithMessage("Longitude must be between -180 and 180.");

        }
    }
}
