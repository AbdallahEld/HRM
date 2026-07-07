using HR.Application.Features.Location.DTOs;
using MediatR;

namespace HR.Application.Features.Location.Queries.GetAllLocations
{
    public class GetAllLocationsQuery : IRequest <IEnumerable<LocationReadDTO>>
    {
    }
}
