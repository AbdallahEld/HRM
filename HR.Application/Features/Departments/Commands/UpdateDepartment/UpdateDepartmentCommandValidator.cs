using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Features.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Department name is required")
                .MaximumLength(100).WithMessage("Department name cannot exceed 100 characters.");

            RuleFor(x => x.ManagerId)
                .GreaterThan(0).When(x => x.ManagerId.HasValue).WithMessage("Manager ID must be greater than 0.");

            RuleFor(x => x.CostCenter)
                .NotEmpty().WithMessage("Cost center is required")
                .MaximumLength(30).WithMessage("Department name cannot exceed 100 characters.");
        }
    }
}
