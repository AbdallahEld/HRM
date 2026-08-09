using HR.Application.Shared;
using HR.Domain.Data.Entities;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.LeaveTypes.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<CreateLeaveTypeCommand, ApiResponse<int>>
    {
        public async Task<ApiResponse<int>> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = new LeaveType
            {
                Name = request.Name,
                MaxDaysPerYear = request.MaxDaysPerYear,
                IsPaid = request.IsPaid,
                CarryOverAllowed = request.CarryOverAllowed,
                RequiresApproval = request.RequiresApproval
            };

            await unitOfWork._LeaveTypeRepository.AddAsync(leaveType);
            await unitOfWork.SaveChangesAsync();

            return ApiResponse<int>.SuccessResponse(leaveType.Id, "Leave type created successfully");
        }
    }
}
