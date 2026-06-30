using HR.Application.Features.Employee.DTOs;
using MediatR;

namespace HR.Application.Features.Employee.Queries.GetAllEmployees
{
    public class GetAllEmployeesQuery : IRequest<IEnumerable<EmployeeReadDTO>>
    {
    }
}
