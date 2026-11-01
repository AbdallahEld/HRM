using HR.Application.Shared;
using HR.Domain.Data.Entities.Identity;
using HR.Domain.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HR.Application.Features.Employee.Commands.PromoteToDepartmentManager
{
    public class PromoteToDepartmentManagerCommandHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager) : IRequestHandler<PromoteToDepartmentManagerCommand, ApiResponse<int>>
    {
        public async Task<ApiResponse<int>> Handle(PromoteToDepartmentManagerCommand request, CancellationToken cancellationToken)
        {
            var employee = await unitOfWork._EmployeeRepository
                .GetAsync(e => e.Id == request.EmployeeId);

            if (employee == null)
                return ApiResponse<int>.FailureResponse(new List<string> { "Employee not found." });

            var department = await unitOfWork._DepartmentRepository
                .GetAsync(d => d.Id == employee.DepartmentId);

            if (department == null)
                return ApiResponse<int>.FailureResponse(new List<string> { "Employee is not assigned to a valid department." });

            if (department.ManagerId == employee.Id)
                return ApiResponse<int>.FailureResponse(new List<string> { "This employee is already the manager of their department." });

            int? oldManagerId = department.ManagerId;

            int? upperManagerId = null;
            if (department.ParentDepartmentId.HasValue)
            {
                var parentDept = await unitOfWork._DepartmentRepository
                    .GetAsync(d => d.Id == department.ParentDepartmentId.Value);
                upperManagerId = parentDept?.ManagerId;
            }

            employee.ManagerId = upperManagerId;
            department.ManagerId = employee.Id;

            unitOfWork._DepartmentRepository.UpdateAsync(department);
            unitOfWork._EmployeeRepository.UpdateAsync(employee);

            await unitOfWork.SaveChangesAsync();

            await unitOfWork._EmployeeRepository.AssignManagerToDepartmentEmployeesAsync(employee.DepartmentId, employee.Id);

            if (oldManagerId.HasValue)
            {
                var oldManagerUser = await userManager.Users
                    .FirstOrDefaultAsync(u => u.EmployeeId == oldManagerId.Value, cancellationToken);

                if (oldManagerUser != null && await userManager.IsInRoleAsync(oldManagerUser, "DepartmentHead"))
                {
                    await userManager.RemoveFromRoleAsync(oldManagerUser, "DepartmentHead");
                }
            }

            var newManagerUser = await userManager.Users
                .FirstOrDefaultAsync(u => u.EmployeeId == employee.Id, cancellationToken);

            if (newManagerUser != null && !await userManager.IsInRoleAsync(newManagerUser, "DepartmentHead"))
            {
                await userManager.AddToRoleAsync(newManagerUser, "DepartmentHead");
            }

            return ApiResponse<int>.SuccessResponse(employee.Id, $"Employee promoted to Manager of '{department.Name}' department successfully.");
        }
    }
}