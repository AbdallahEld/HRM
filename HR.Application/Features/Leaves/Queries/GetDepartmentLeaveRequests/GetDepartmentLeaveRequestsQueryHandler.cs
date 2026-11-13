using HR.Application.Features.Leave.DTOs;
using HR.Application.Shared;
using HR.Domain.UnitOfWork;
using MediatR;
using System.Linq.Expressions;

namespace HR.Application.Features.Leave.Queries.GetDepartmentLeaveRequests
{
    public class GetDepartmentLeaveRequestsQueryHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<GetDepartmentLeaveRequestsQuery, ApiResponse<List<LeavesReadDTO>>>
    {
        public async Task<ApiResponse<List<LeavesReadDTO>>> Handle(GetDepartmentLeaveRequestsQuery request, CancellationToken cancellationToken)
        {
            var employee = await unitOfWork._EmployeeRepository.GetByIdAsync(request.ManagerId);

            var departmentId = employee.DepartmentId;

            var leaveRequests = await unitOfWork._LeaveRepository.GetAllAsync(
                filter: l => l.Employee.DepartmentId == departmentId,
                sortBy: null,
                isDescending: false,
                trackChanges: false,
                includes: new Expression<Func<HR.Domain.Data.Entities.Leave, object>>[]
                {
                    l => l.Employee,
                    l => l.LeaveType
                });

            var leaveRequestsDTO = leaveRequests.Select(l => new LeavesReadDTO
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

            return ApiResponse<List<LeavesReadDTO>>.SuccessResponse(leaveRequestsDTO);
        }
    }
}
