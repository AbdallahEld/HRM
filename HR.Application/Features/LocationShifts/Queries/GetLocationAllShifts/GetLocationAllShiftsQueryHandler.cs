using HR.Application.Features.Shift.DTOs;
using HR.Application.Shared;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.LocationShifts.Queries.GetLocationAllShifts
{
    public class GetLocationAllShiftsQueryHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<GetLocationAllShiftsQuery, ApiResponse<IEnumerable<ShiftReadDTO>>>
    {
        public async Task<ApiResponse<IEnumerable<ShiftReadDTO>>> Handle(GetLocationAllShiftsQuery request, CancellationToken cancellationToken)
        {
            var shifts = await unitOfWork._LocationShiftsRepositroy.GetAllAsync(
                filter: x => x.LocationId == request.LocationId,
                includes: x => x.Shift
                );

            if ( shifts == null )
            {
                return ApiResponse<IEnumerable<ShiftReadDTO>>.FailureResponse(new List<string> { "No shifts found for the specified location." }, "No shifts found");
            }

            List<ShiftReadDTO> result = shifts.Select(s => new ShiftReadDTO
            {
                Id = s.Shift.Id,
                Name = s.Shift.Name,
                IsFlexible = s.Shift.IsFlexible,
                RequiredHours = s.Shift.RequiredHours,
                StartTime = s.Shift.StartTime,
                EndTime = s.Shift.EndTime,
                GracePeriodMinutes = s.Shift.GracePeriodMinutes
            }).ToList();

            return ApiResponse<IEnumerable<ShiftReadDTO>>.SuccessResponse(result, "Shifts retrieved successfully");
        }
    }
}
