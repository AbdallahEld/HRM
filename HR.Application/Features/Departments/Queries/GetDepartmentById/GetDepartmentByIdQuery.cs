using HR.Application.Features.Departments.DTOs;
using HR.Application.Shared;
using MediatR;

namespace HR.Application.Features.Departments.Queries.GetDepartmentById
{
    public class GetDepartmentByIdQuery(int id) : IRequest<ApiResponse<DepartmentReadDTO>>
    {
        public int Id { get; } = id;
    }
}
