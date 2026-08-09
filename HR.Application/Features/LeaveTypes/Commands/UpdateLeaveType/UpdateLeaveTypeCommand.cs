using HR.Application.Features.LeaveTypes.Commands.Shared;
using HR.Application.Shared;
using MediatR;

namespace HR.Application.Features.LeaveTypes.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommand : IRequest<ApiResponse<int>> , ILeaveTypeCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxDaysPerYear { get; set; }
        public bool IsPaid { get; set; }
        public bool CarryOverAllowed { get; set; }
        public bool RequiresApproval { get; set; }
    }
}
