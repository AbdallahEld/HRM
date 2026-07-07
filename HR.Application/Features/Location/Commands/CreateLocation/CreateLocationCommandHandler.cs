using HR.Domain.Data.Entities;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.Location.Commands.CreateLocation
{
    public class CreateLocationCommandHandler (
        IUnitOfWork unitOfWork): IRequestHandler<CreateLocationCommand, int>
    {
        public async Task<int> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = new Domain.Data.Entities.Location
            {
                IsRemote = request.IsRemote,
                Address = request.Address,
                Lat = request.Lat,
                Long = request.Long
            };

            await unitOfWork._LocationRepository.AddAsync(location);
            await unitOfWork.SaveChangesAsync();

            return location.Id;
        }
    }
}
