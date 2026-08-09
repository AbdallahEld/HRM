using HR.Application.Shared;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.LeaveTypes.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteLeaveTypeCommand, ApiResponse<int>>
    {
        public async Task<ApiResponse<int>> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = await unitOfWork._LeaveTypeRepository.GetByIdAsync(request.Id);
            if (leaveType == null)
            {
                return ApiResponse<int>.FailureResponse(new List<string> { "Leave type not found" }, "Delete operation failed");
            }

            unitOfWork._LeaveTypeRepository.DeleteAsync(leaveType);
            await unitOfWork.SaveChangesAsync();

            return ApiResponse<int>.SuccessResponse(request.Id, "Leave type deleted successfully");
        }
    }
}
