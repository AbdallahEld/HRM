using MediatR;

namespace HR.Application.Features.Departments.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
