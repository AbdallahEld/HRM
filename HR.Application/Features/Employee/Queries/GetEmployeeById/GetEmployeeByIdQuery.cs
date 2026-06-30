using HR.Application.Features.Employee.DTOs;
using MediatR;

namespace HR.Application.Features.Employee.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQuery (int id) : IRequest<EmployeeReadDTO>
    {
        public int Id { get; } = id;
    }
}
