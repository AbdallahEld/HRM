using HR.Application.Departments.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Departments.Queries.GetAllDepartments
{
    public class GetAllDepartmentsQuery : IRequest<IEnumerable<DepartmentReadDTO>>
    {
    }
}
