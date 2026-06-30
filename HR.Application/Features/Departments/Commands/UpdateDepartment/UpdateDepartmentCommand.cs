using MediatR;

namespace HR.Application.Features.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CostCenter { get; set; }
        public int HeadCount { get; set; } 
        public int? ParentDepartmentId { get; set; }
        public int? ManagerId { get; set; }
    }
}
