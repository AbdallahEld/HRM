using HR.Application.Features.LeaveTypes.DTOs;
using HR.Application.Shared;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.LeaveTypes.Queries.GetAllLeaveTypes
{
    public class GetAllLeaveTypesQueryHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<GetAllLeaveTypesQuery, ApiResponse<IEnumerable<LeaveTypeReadDTO>>>
    {
        public async Task<ApiResponse<IEnumerable<LeaveTypeReadDTO>>> Handle(GetAllLeaveTypesQuery request, CancellationToken cancellationToken)
        {
            var leaveTypes = await unitOfWork._LeaveTypeRepository.GetAllAsync();

            var leaveTypesDTOs = leaveTypes.Select(lt => new LeaveTypeReadDTO
            {
                Id = lt.Id,
                Name = lt.Name,
                MaxDaysPerYear = lt.MaxDaysPerYear,
                IsPaid = lt.IsPaid,
                CarryOverAllowed = lt.CarryOverAllowed,
                RequiresApproval = lt.RequiresApproval
            });

            return ApiResponse<IEnumerable<LeaveTypeReadDTO>>.SuccessResponse(leaveTypesDTOs, "Leave types retrieved successfully.");
        }
    }
}
