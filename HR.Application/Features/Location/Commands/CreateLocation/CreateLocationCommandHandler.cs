using HR.Application.Shared;
using HR.Domain.Data.Entities;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.Location.Commands.CreateLocation
{
    public class CreateLocationCommandHandler (
        IUnitOfWork unitOfWork): IRequestHandler<CreateLocationCommand, ApiResponse<int>>
    {
        public async Task<ApiResponse<int>> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
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

            return ApiResponse<int>.SuccessResponse(location.Id, "Location created successfully");
        }
    }
}
