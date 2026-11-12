using HR.Application.Features.Leave.DTOs;
using HR.Application.Shared;
using MediatR;

namespace HR.Application.Features.Leave.Queries.GetDepartmentLeaveRequests
{
    public class GetDepartmentLeaveRequestsQuery : IRequest<ApiResponse<List<LeavesReadDTO>>>
    {
        public int ManagerId { get; set; }
    }
}
