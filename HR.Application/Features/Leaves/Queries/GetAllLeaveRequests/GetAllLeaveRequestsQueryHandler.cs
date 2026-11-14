using HR.Application.Features.Leave.DTOs;
using HR.Application.Shared;
using HR.Domain.UnitOfWork;
using MediatR;
using System.Linq.Expressions;

namespace HR.Application.Features.Leaves.Queries.GetAllLeaveRequests
{
    public class GetAllLeaveRequestsQueryHandler (
        IUnitOfWork unitOfWork ) : IRequestHandler<GetAllLeaveRequestsQuery, ApiResponse<IEnumerable<LeavesReadDTO>>>
    {
        public async Task<ApiResponse<IEnumerable<LeavesReadDTO>>> Handle(GetAllLeaveRequestsQuery request, CancellationToken cancellationToken)
        {
            var leaves = await unitOfWork._LeaveRepository.GetAllAsync(includes: new Expression<Func<HR.Domain.Data.Entities.Leave, object>>[]
            {
                l => l.Employee,
                l => l.LeaveType
            });

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
            });

            return ApiResponse<IEnumerable
                <LeavesReadDTO>>.SuccessResponse(leavesDTO);
        }
    }
}
