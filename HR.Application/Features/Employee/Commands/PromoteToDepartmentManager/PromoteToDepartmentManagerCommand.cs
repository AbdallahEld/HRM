using HR.Application.Shared;
using MediatR;

namespace HR.Application.Features.Employee.Commands.PromoteToDepartmentManager
{
    public class PromoteToDepartmentManagerCommand : IRequest<ApiResponse<int>>
    {
        public int EmployeeId { get; set; }
    }
}
