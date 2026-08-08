using HR.Application.Features.LocationShifts.DTOs;
using HR.Application.Shared;
using MediatR;

namespace HR.Application.Features.LocationShifts.Commands.AssignShiftToLocation
{
    public class AssignShiftToLocationCommand : IRequest<ApiResponse<ShiftLocationDTO>>
    {
        public int ShiftId { get; set; }
        public int LocationId { get; set; }
    }
}
