using HR.Application.Shared;
using MediatR;

namespace HR.Application.Features.LeaveTypes.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommand (int id) : IRequest<ApiResponse<int>>
    {
        public int Id { get; } = id;
    }
}
