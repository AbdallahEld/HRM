using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Shift.Commands.Shared
{
    public class ShiftCommandValidatorBase<T> : AbstractValidator<T> where T : IShiftCommand
    {
        protected ShiftCommandValidatorBase()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("Shift name is required")
                .MaximumLength(100).WithMessage("Shift name cannot exceed 100 characters.");

            RuleFor(s => s.RequiredHours)
                .InclusiveBetween(1, 24)
                .When(s => s.RequiredHours.HasValue)
                .WithMessage("Required hours must be between 1 and 24.");

            RuleFor(s => s)
                .Must(MatchTimeDifference)
                .When(s => s.StartTime.HasValue && s.EndTime.HasValue && s.RequiredHours.HasValue)
                .WithName("RequiredHours")
                .WithMessage("Required hours must exactly match the difference between Start Time and End Time.");

            RuleFor(s => s.StartTime)
                .NotNull()
                .When(s => !s.IsFlexible)
                .WithMessage("Start Time is required for fixed shifts.");

            RuleFor(s => s.EndTime)
                .NotNull()
                .When(s => !s.IsFlexible)
                .WithMessage("End Time is required for fixed shifts.");

            RuleFor(s => s.GracePeriodMinutes)
                .GreaterThanOrEqualTo(0).WithMessage("Shift grace period cannot be less than 0");
        }

        private bool MatchTimeDifference(T command)
        {
            var start = command.StartTime.Value;
            var end = command.EndTime.Value;
            TimeSpan duration = end - start;

            if (duration.TotalHours < 0)
            {
                duration = duration.Add(TimeSpan.FromHours(24));
            }

            return command.RequiredHours.Value == (int)duration.TotalHours;
        }
    }
}
