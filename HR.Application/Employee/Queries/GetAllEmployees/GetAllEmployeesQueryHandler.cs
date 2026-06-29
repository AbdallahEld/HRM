using HR.Application.Employee.DTOs;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Employee.Queries.GetAllEmployees
{
    public class GetAllEmployeesQueryHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeeReadDTO>>
    {
        public async Task<IEnumerable<EmployeeReadDTO>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await unitOfWork._EmployeeRepository.GetAllAsync();

            var employeesReadDTO = employees.Select(e => new EmployeeReadDTO
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                DateOfBirth = e.DateOfBirth,
                Gender = e.Gender,
                PhoneNumber = e.PhoneNumber,
                NationalId = e.NationalId,
                EmploymentType = e.EmploymentType,
                EmploymentStatus = e.EmploymentStatus,
                ProbationEndDate = e.ProbationEndDate,
                BaseSalary = e.BaseSalary,
                HourlyRate = e.HourlyRate,
                Position = e.Position,
                HireDate = e.HireDate,
                Nationality = e.Nationality,
                ManagerId = e.ManagerId,
                DepartmentId = e.DepartmentId,
                DefaultShiftId = e.DefaultShiftId
            });
            
            return employeesReadDTO;
        }
    }
}
