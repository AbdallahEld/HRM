using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Departments.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommand (int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
