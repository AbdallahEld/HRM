using HR.Application.Features.Departments.DTOs;
using HR.Application.Shared;
using MediatR;

namespace HR.Application.Features.Departments.Queries.GetAllDepartments
{
    public class GetAllDepartmentsQuery : IRequest<ApiResponse<IEnumerable<DepartmentReadDTO>>>
    {
    }
}
