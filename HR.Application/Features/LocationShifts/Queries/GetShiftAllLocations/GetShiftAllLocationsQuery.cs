using HR.Application.Features.Location.DTOs;
using HR.Application.Shared;
using MediatR;

namespace HR.Application.Features.LocationShifts.Queries.GetShiftAllLocations
{
    public class GetShiftAllLocationsQuery (int shiftId) : IRequest<ApiResponse<IEnumerable<LocationReadDTO>>>
    {
        public int ShiftId { get; } = shiftId;
    }
}
