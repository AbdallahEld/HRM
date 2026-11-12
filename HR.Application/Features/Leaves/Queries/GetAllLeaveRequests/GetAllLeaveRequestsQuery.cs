using HR.Application.Features.Leave.DTOs;
using HR.Application.Shared;
using MediatR;

namespace HR.Application.Features.Leaves.Queries.GetAllLeaveRequests
{
    public class GetAllLeaveRequestsQuery : IRequest<ApiResponse<List<LeavesReadDTO>>>
    {
    }
}
