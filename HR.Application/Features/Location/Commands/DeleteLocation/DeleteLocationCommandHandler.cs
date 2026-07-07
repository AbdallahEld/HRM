using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.Location.Commands.DeleteLocation
{
    public class DeleteLocationCommandHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteLocationCommand>
    {
        public async Task Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await unitOfWork._LocationRepository.GetByIdAsync(request.Id);
            if (location == null)
            {
                throw new Exception($"Location with Id = {request.Id} not Found");
            }

            unitOfWork._LocationRepository.DeleteAsync(location);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
