using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommand : IRequest<int>
    {
        public string Name { get; set; } 
        public string CostCenter { get; set; }
        public int HeadCount { get; set; } = 0;
        public int? ParentDepartmentId { get; set; }
        public int? ManagerId { get; set; }
    }
}
