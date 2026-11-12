using HR.Application.Features.Leave.DTOs;
using HR.Application.Shared;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.Leaves.Queries.GetAllLeaveRequests
{
    public class GetAllLeaveRequestsQueryHandler (
        IUnitOfWork unitOfWork ) : IRequestHandler<GetAllLeaveRequestsQuery, ApiResponse<List<LeavesReadDTO>>>
    {
        public async Task<ApiResponse<List<LeavesReadDTO>>> Handle(GetAllLeaveRequestsQuery request, CancellationToken cancellationToken)
        {
            var leaves = await unitOfWork._LeaveRepository.GetAllAsync();

            var leavesDTO = leaves.Select(l => new LeavesReadDTO
            {
                Id = l.Id,
                EmployeeId = l.EmployeeId,
                EmployeeName = l.Employee.FirstName + " " + l.Employee.LastName,
                LeaveType = l.LeaveType.Name,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                Status = l.Status,
                TotalWorkingDays = l.TotalDays
            }).ToList();

            return ApiResponse<List<LeavesReadDTO>>.SuccessResponse(leavesDTO);
        }
    }
}
