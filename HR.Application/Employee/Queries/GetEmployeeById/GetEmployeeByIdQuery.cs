using HR.Application.Employee.DTOs;
using MediatR;

namespace HR.Application.Employee.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQuery (int id) : IRequest<EmployeeReadDTO>
    {
        public int Id { get; } = id;
    }
}
