using HR.Application.Features.LeaveTypes.DTOs;
using HR.Application.Shared;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.LeaveTypes.Queries.GetLeaveTypeById
{
    public class GetLeaveTypeByIdQueryHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<GetLeaveTypeByIdQuery, ApiResponse<LeaveTypeReadDTO>>
    {
        public async Task<ApiResponse<LeaveTypeReadDTO>> Handle(GetLeaveTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var leaveType = await unitOfWork._LeaveTypeRepository.GetByIdAsync(request.Id);
            
            if (leaveType == null)
            {
                return ApiResponse<LeaveTypeReadDTO>.FailureResponse(new List<string> { "Leave type not found" }, "Leave type not found");
            }

            var leaveTypeDTO = new LeaveTypeReadDTO
            {
                Id = leaveType.Id,
                Name = leaveType.Name,
                MaxDaysPerYear = leaveType.MaxDaysPerYear,
                IsPaid = leaveType.IsPaid,
                CarryOverAllowed = leaveType.CarryOverAllowed,
                RequiresApproval = leaveType.RequiresApproval
            };

            return ApiResponse<LeaveTypeReadDTO>.SuccessResponse(leaveTypeDTO, "Leave type retrieved successfully");
        }
    }
}
