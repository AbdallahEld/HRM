using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.Location.Commands.UpdateLocation
{
    public class UpdateLocationCommandHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateLocationCommand, int>
    {
        public async Task<int> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await unitOfWork._LocationRepository.GetByIdAsync(request.Id);
            if (location == null)
            {
                throw new Exception($"Location with ID {request.Id} not found.");
            }

            location.Address = request.Address;
            location.IsRemote = request.IsRemote;
            location.Lat = request.Lat;
            location.Long = request.Long;

            unitOfWork._LocationRepository.UpdateAsync(location);
            await unitOfWork.SaveChangesAsync();

            return location.Id;
        }
    }
}
