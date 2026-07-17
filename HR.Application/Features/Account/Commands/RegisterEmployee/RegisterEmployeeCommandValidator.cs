using FluentValidation;

namespace HR.Application.Features.Account.Commands.RegisterEmployee
{
    public class RegisterEmployeeCommandValidator : AbstractValidator<RegisterEmployeeCommand>
    {
        public RegisterEmployeeCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.")
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^01[0125][0-9]{8}$").WithMessage("Phone number must be a valid Egyptian mobile number (11 digits starting with 010, 011, 012, or 015).");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .Must(BeAtLeast18YearsOld).WithMessage("Employee must be at least 18 years old.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Invalid gender selection.");

            RuleFor(x => x.NationalId)
                .NotEmpty().WithMessage("National ID is required.")
                .Length(14).WithMessage("National ID must be exactly 14 digits.")
                .Matches("^[0-9]*$").WithMessage("National ID must contain only numbers.");

            RuleFor(x => x.Nationality)
                .NotEmpty().WithMessage("Nationality is required.")
                .MaximumLength(50).WithMessage("Nationality must not exceed 50 characters.");

            RuleFor(x => x.EmploymentType)
                .IsInEnum().WithMessage("Invalid employment type.");

            RuleFor(x => x.EmploymentStatus)
                .IsInEnum().WithMessage("Invalid employment status.");

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Position is required.")
                .MaximumLength(100).WithMessage("Position must not exceed 100 characters.");

            RuleFor(x => x.HireDate)
                .NotEmpty().WithMessage("Hire date is required.");

            RuleFor(x => x.ProbationEndDate)
                .Must(BeAfterHireDate).When(x => x.ProbationEndDate.HasValue)
                .WithMessage("Probation end date must be after the hire date.");

            RuleFor(x => x.BaseSalary)
                .Empty().When(x => x.EmploymentType != Domain.Data.Entities.Enums.EmploymentType.FullTime)
                .WithMessage("Non Full-Time employees cannot have a Base Salary. Please provide an Hourly Rate instead.");

            RuleFor(x => x)
                .Must(x => x.BaseSalary.HasValue ^ x.HourlyRate.HasValue)
                .WithMessage("You must provide EITHER BaseSalary OR HourlyRate, but not both or neither.");

            RuleFor(x => x.BaseSalary)
                .GreaterThan(0).When(x => x.BaseSalary.HasValue)
                .WithMessage("Base salary must be greater than zero.");

            RuleFor(x => x.HourlyRate)
                .GreaterThan(0).When(x => x.HourlyRate.HasValue)
                .WithMessage("Hourly rate must be greater than zero.");

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).WithMessage("A valid Department ID is required.");

            RuleFor(x => x.DefaultShiftId)
                .GreaterThan(0).WithMessage("A valid Default Shift ID is required.");

            RuleFor(x => x.ManagerId)
                .GreaterThan(0).When(x => x.ManagerId.HasValue)
                .WithMessage("Manager ID must be greater than zero if provided.");
        }

        private bool BeAtLeast18YearsOld(DateOnly dateOfBirth)
        {
            var minAllowedDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-18));
            return dateOfBirth <= minAllowedDate;
        }

        private bool BeAfterHireDate(RegisterEmployeeCommand command, DateTime? probationEndDate)
        {
            if (!probationEndDate.HasValue) return true;

            var hireDateTime = command.HireDate.ToDateTime(TimeOnly.MinValue);
            return probationEndDate.Value > hireDateTime;
        }
    }
}
