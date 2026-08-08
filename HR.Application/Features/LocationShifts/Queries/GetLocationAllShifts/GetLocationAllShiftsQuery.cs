using HR.Application.Features.Shift.DTOs;
using HR.Application.Shared;
using MediatR;

namespace HR.Application.Features.LocationShifts.Queries.GetLocationAllShifts
{
    public class GetLocationAllShiftsQuery (int locationId) : IRequest<ApiResponse<IEnumerable<ShiftReadDTO>>>
    {
        public int LocationId { get; } = locationId;
    }
}
