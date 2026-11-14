using HR.Application.Features.Leaves.Services.ValidateActualManager;
using HR.Application.Shared;
using HR.Domain.Data.Entities.Enums;
using HR.Domain.Events.Employee;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.Leaves.Commands.ApproveEmployeeLeave
{
    public class ApproveEmployeeLeaveCommandHandler (
        IUnitOfWork unitOfWork,
        IValidateActualManager validateActualManager,
        IMediator mediator) : IRequestHandler<ApproveEmployeeLeaveCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(ApproveEmployeeLeaveCommand request, CancellationToken cancellationToken)
        {
            var leave = await unitOfWork._LeaveRepository.GetByIdAsync(request.LeaveId);
            if (leave == null)
            {
                return ApiResponse<string>.FailureResponse(new List<string>
                {
                    "Leave request not found."
                });
            }

            if (leave.Status != LeaveStatus.Pending)
            {
                return ApiResponse<string>.FailureResponse(new List<string>
                {
                    "Leave request is not pending and cannot be approved."
                });
            }

            var valResult = await validateActualManager.ValidateManager(request.ManagerId, leave.EmployeeId);
            if (!valResult.Success)
            {
                return valResult;
            }

            leave.Status = LeaveStatus.Approved;
            leave.ApprovedAt = DateOnly.FromDateTime(DateTime.UtcNow);
            leave.ApproverId = request.ManagerId;

            unitOfWork._LeaveRepository.UpdateAsync(leave);
            
            await mediator.Publish(new EmployeeLeaveApproved(leave), cancellationToken);
            return ApiResponse<string>.SuccessResponse("Leave request approved successfully.");
        }
    }
}
