using HR.Application.Features.Location.DTOs;
using HR.Application.Shared;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.LocationShifts.Queries.GetShiftAllLocations
{
    public class GetShiftAllLocationsQueryHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<GetShiftAllLocationsQuery, ApiResponse<IEnumerable<LocationReadDTO>>>
    {
        public async Task<ApiResponse<IEnumerable<LocationReadDTO>>> Handle(GetShiftAllLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = await unitOfWork._LocationShiftsRepositroy.GetAllAsync(
                filter: ls => ls.ShiftId == request.ShiftId,
                includes: ls => ls.Location
                );

            if ( locations == null )
            {
                return ApiResponse<IEnumerable<LocationReadDTO>>.FailureResponse(new List<string> { "No locations found for the specified shift." }, "No locations found");
            }

            var result = locations.Select(ls => new LocationReadDTO
            {
                Id = ls.Location.Id,
                IsRemote = ls.Location.IsRemote,
                Address = ls.Location.Address,
                Lat = ls.Location.Lat,
                Long = ls.Location.Long
            });

            return ApiResponse<IEnumerable<LocationReadDTO>>.SuccessResponse(result, "Locations retrieved successfully");
        }
    }
}
