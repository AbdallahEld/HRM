using HR.Application.Features.Leaves.Services.ValidateLeaveBalance;
using HR.Application.Features.Leaves.Services.ValidateNoOverlap;
using HR.Application.Shared;
using HR.Domain.Data.Entities;
using HR.Domain.Data.Entities.Enums;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.Leaves.Commands.SubmitLeaveRequest
{
    public class SubmitLeaveRequestCommandHandler (
        IUnitOfWork unitOfWork,
        INoOverlapChecker noOverlapChecker,
        ILeaveBalanceChecker leaveBalanceChecker) : IRequestHandler<SubmitLeaveRequestCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(SubmitLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveType = await unitOfWork._LeaveTypeRepository.GetAsync(leaveType => leaveType.Id == request.LeaveTypeId);
            if (leaveType == null)
                return ApiResponse<string>.FailureResponse(new List<string> 
                {
                    "Leave type not found."
                });

            var existingLeaves = await unitOfWork._LeaveRepository
                .GetAllAsync(leave => leave.EmployeeId == request.EmployeeId && leave.LeaveTypeId == request.LeaveTypeId);

            var result = noOverlapChecker
                .ValidateNoOverlap(request.StartDate , request.EndDate , existingLeaves);

            if (!result.Success)
            {
                return ApiResponse<string>.FailureResponse(result.Errors);
            }

            var requestedDays = 0;

            for (var date = request.StartDate; date <= request.EndDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Friday)
                {
                    requestedDays++;
                }
            }

            var result2 = await leaveBalanceChecker
                .ValidateLeaveBalanceAsync(request.LeaveTypeId, requestedDays,request.EmployeeId);

            if (!result2.Success)
            {
                return ApiResponse<string>.FailureResponse(result2.Errors);
            }

            var leaveRequest = new Leave
            {
                EmployeeId = request.EmployeeId,
                LeaveTypeId = request.LeaveTypeId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Reason = request.Reason,
                Status = LeaveStatus.Pending,
                TotalDays = requestedDays
            };

            return ApiResponse<string>.SuccessResponse("Leave request submitted successfully.");
        }
    }
}
