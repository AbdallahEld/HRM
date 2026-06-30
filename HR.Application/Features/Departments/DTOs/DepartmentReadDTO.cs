using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Features.Departments.DTOs
{
    public class DepartmentReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CostCenter { get; set; }
        public int HeadCount { get; set; } = 0;
        public int? ParentDepartmentId { get; set; }
        public int? ManagerId { get; set; }
    }
}
