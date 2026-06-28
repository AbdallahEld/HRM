using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Department name is required")
                .MinimumLength(100).WithMessage("Department name cannot exceed 100 characters.");

            RuleFor(x => x.ManagerId)
                .GreaterThan(0).When(x => x.ManagerId.HasValue).WithMessage("Manager ID must be greater than 0.");
        }
    }
}
