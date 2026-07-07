using HR.Application.Features.Location.DTOs;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.Location.Queries.GetAllLocations
{
    public class GetAllLocationsQueryHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<GetAllLocationsQuery, IEnumerable<LocationReadDTO>>
    {
        public async Task<IEnumerable<LocationReadDTO>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = await unitOfWork._LocationRepository.GetAllAsync();

            var locationDTOs = locations.Select(location => new LocationReadDTO
            {
                Id = location.Id,
                IsRemote = location.IsRemote,
                Address = location.Address,
                Lat = location.Lat,
                Long = location.Long
            });

            return locationDTOs;
        }
    }
}
