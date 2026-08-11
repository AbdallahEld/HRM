using HR.Application.Shared;
using HR.Domain.Data.Entities.Identity;
using HR.Domain.Events.Employee;
using HR.Domain.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HR.Application.Features.Account.Commands.RegisterEmployee
{
    public class RegisterEmployeeCommandHandler(
        UserManager<User> userManager,
        IUnitOfWork unitOfWork,
        IMediator mediator) : IRequestHandler<RegisterEmployeeCommand, ApiResponse<int>>
    {
        public async Task<ApiResponse<int>> Handle(RegisterEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return ApiResponse<int>.FailureResponse(new List<string> { "Email already exists." }, "Registration failed");
            }
            
            decimal? calculatedHourlyRate = request.HourlyRate;
            if (request.BaseSalary.HasValue)
            {
                var shift = await unitOfWork._ShiftRepository.GetByIdAsync(request.DefaultShiftId);
                calculatedHourlyRate = request.BaseSalary.Value / (shift.RequiredHours * 22);
            }

            var newEmployee = new Domain.Data.Entities.Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                NationalId = request.NationalId,
                Nationality = request.Nationality,
                EmploymentType = request.EmploymentType,
                EmploymentStatus = request.EmploymentStatus,
                Position = request.Position,
                HireDate = request.HireDate,
                ProbationEndDate = request.ProbationEndDate,
                BaseSalary = request.BaseSalary,
                HourlyRate = calculatedHourlyRate,
                DepartmentId = request.DepartmentId,
                DefaultShiftId = request.DefaultShiftId,
                ManagerId = request.ManagerId,
            };

            var newUser = new User
            {
                UserName = request.Email,
                Email = request.Email,
                Employee = newEmployee,
            };

            var identityResult = await userManager.CreateAsync(newUser, request.Password);

            if (!identityResult.Succeeded)
            {
                return ApiResponse<int>.FailureResponse(identityResult.Errors.Select(e => e.Description).ToList(), "Registration failed");
            }

            await userManager.AddToRoleAsync(newUser, "Employee");

            await mediator.Publish(new EmployeeRegistered(newEmployee), cancellationToken);
            return ApiResponse<int>.SuccessResponse(newEmployee.Id, "Employee registered successfully");
        }
    }
}
