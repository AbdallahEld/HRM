using HR.Application.Shared;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.LeaveTypes.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateLeaveTypeCommand, ApiResponse<int>>
    {
        public async Task<ApiResponse<int>> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = await unitOfWork._LeaveTypeRepository.GetByIdAsync(request.Id);

            if (leaveType == null)
            {
                return ApiResponse<int>.FailureResponse(new List<string> { "Leave type not found." }, "Update failed");
            }

            leaveType.Name = request.Name;
            leaveType.MaxDaysPerYear = request.MaxDaysPerYear;
            leaveType.IsPaid = request.IsPaid;
            leaveType.CarryOverAllowed = request.CarryOverAllowed;
            leaveType.RequiresApproval = request.RequiresApproval;

            unitOfWork._LeaveTypeRepository.UpdateAsync(leaveType);
            await unitOfWork.SaveChangesAsync();

            return ApiResponse<int>.SuccessResponse(leaveType.Id, "Leave type updated successfully");
        }
    }
}
