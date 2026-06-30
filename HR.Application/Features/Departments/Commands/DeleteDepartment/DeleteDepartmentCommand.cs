using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Features.Departments.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommand (int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
