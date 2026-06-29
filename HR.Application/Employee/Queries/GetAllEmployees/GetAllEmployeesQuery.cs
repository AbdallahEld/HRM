using HR.Application.Employee.DTOs;
using MediatR;

namespace HR.Application.Employee.Queries.GetAllEmployees
{
    public class GetAllEmployeesQuery : IRequest<IEnumerable<EmployeeReadDTO>>
    {
    }
}
