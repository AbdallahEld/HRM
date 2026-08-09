using HR.Application.Features.LeaveTypes.Commands.Shared;
using HR.Application.Shared;
using MediatR;

namespace HR.Application.Features.LeaveTypes.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommand : IRequest<ApiResponse<int>>, ILeaveTypeCommand
    {
        public string Name { get; set; }
        public int MaxDaysPerYear { get; set; }
        public bool IsPaid { get; set; }
        public bool CarryOverAllowed { get; set; }
        public bool RequiresApproval { get; set; }
    }
}
