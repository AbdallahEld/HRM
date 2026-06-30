using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Shift.Commands.DeleteShift
{
    public class DeleteShiftCommandHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteShiftCommand>
    {
        public async Task Handle(DeleteShiftCommand request, CancellationToken cancellationToken)
        {
            var shift = await unitOfWork._ShiftRepository.GetByIdAsync(request.Id);

            if (shift == null)
            {
                throw new Exception($"Shift With Id = {request.Id} is not Found");
            }

            unitOfWork._ShiftRepository.DeleteAsync(shift);
            await unitOfWork.SaveChangesAsync();
        }
    }
}

