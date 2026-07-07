using HR.Application.Features.Location.DTOs;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.Location.Queries.GetLocationById
{
    public class GetLocationByIdQueryHandler (
        IUnitOfWork unitOfWork): IRequestHandler<GetLocationByIdQuery, LocationReadDTO>
    {
        public async Task<LocationReadDTO> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var location = await unitOfWork._LocationRepository.GetByIdAsync(request.Id);
            if(location == null)
            {
                throw new Exception($"Location with Id {request.Id} not found.");
            }

            var locationReadDTO = new LocationReadDTO
            {
                Id = location.Id,
                IsRemote = location.IsRemote,
                Address = location.Address,
                Lat = location.Lat,
                Long = location.Long,
            };

            return locationReadDTO;
        }
    }
}
