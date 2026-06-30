using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Shift.Commands.UpdateShift
{
    public class UpdateShiftCommandHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateShiftCommand, int>
    {
        public async Task<int> Handle(UpdateShiftCommand request, CancellationToken cancellationToken)
        {
            var shift = await unitOfWork._ShiftRepository.GetByIdAsync(request.Id);

            if(shift == null) 
            {
                throw new Exception($"Shift With Id = {request.Id} is not Found");
            }

            shift.Name = request.Name;
            shift.IsFlexible = request.IsFlexible;
            shift.RequiredHours = request.RequiredHours;
            shift.StartTime = request.StartTime;
            shift.EndTime = request.EndTime;
            shift.GracePeriodMinutes = request.GracePeriodMinutes;

            unitOfWork._ShiftRepository.UpdateAsync(shift);
            await unitOfWork.SaveChangesAsync();

            return shift.Id;
        }
    }
}
