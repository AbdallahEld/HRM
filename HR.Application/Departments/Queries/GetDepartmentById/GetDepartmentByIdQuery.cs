using HR.Application.Departments.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Departments.Queries.GetDepartmentById
{
    public class GetDepartmentByIdQuery (int id) : IRequest<DepartmentReadDTO>
    {
        public int Id { get; } = id;
    }
}
