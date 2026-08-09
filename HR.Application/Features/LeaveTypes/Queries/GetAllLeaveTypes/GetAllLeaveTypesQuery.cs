using HR.Application.Features.LeaveTypes.DTOs;
using HR.Application.Shared;
using MediatR;

namespace HR.Application.Features.LeaveTypes.Queries.GetAllLeaveTypes
{
    public class GetAllLeaveTypesQuery : IRequest<ApiResponse<IEnumerable<LeaveTypeReadDTO>>>
    {
    }
}
