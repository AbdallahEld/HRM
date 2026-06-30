using HR.Application.Features.Employee.DTOs;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.Employee.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<GetEmployeeByIdQuery, EmployeeReadDTO>
    {
        public async Task<EmployeeReadDTO> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await unitOfWork._EmployeeRepository.GetByIdAsync(request.Id);
            if (employee == null)
            {
                throw new Exception($"Employee with Id {request.Id} not found.");
            }

            var employeeReadDTO = new EmployeeReadDTO()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Gender = employee.Gender,
                PhoneNumber = employee.PhoneNumber,
                NationalId = employee.NationalId,
                EmploymentType = employee.EmploymentType,
                EmploymentStatus = employee.EmploymentStatus,
                ProbationEndDate = employee.ProbationEndDate,
                BaseSalary = employee.BaseSalary,
                HourlyRate = employee.HourlyRate,
                Position = employee.Position,
                HireDate = employee.HireDate,
                Nationality = employee.Nationality,
                ManagerId = employee.ManagerId,
                DepartmentId = employee.DepartmentId,
                DefaultShiftId = employee.DefaultShiftId
            };

            return employeeReadDTO;
        }
    }
}
