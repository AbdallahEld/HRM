using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Shift.Commands.CreateShift
{
    public class CreateShiftCommandHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<CreateShiftCommand, int>
    {
        public async Task<int> Handle(CreateShiftCommand request, CancellationToken cancellationToken)
        {
            var shift = new HR.Domain.Data.Entities.Shift
            {
                Name = request.Name,
                IsFlexible = request.IsFlexible,
                RequiredHours = request.RequiredHours,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                GracePeriodMinutes = request.GracePeriodMinutes
            };

            await unitOfWork._ShiftRepository.AddAsync(shift);
            await unitOfWork.SaveChangesAsync();
            return shift.Id;
        }
    }
}
