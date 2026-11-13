using HR.Application.Shared;
using MediatR;

namespace HR.Application.Features.Leaves.Commands.ApproveEmployeeLeave
{
    public class ApproveEmployeeLeaveCommand (
        int leaveId,
        int managerId) : IRequest<ApiResponse<string>>
    {
        public int LeaveId { get; } = leaveId;
        public int ManagerId { get; } = managerId;
    }
}
