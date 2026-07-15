using HR.Application.Shared;
using HR.Domain.Data.Entities;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.Shift.Commands.UpdateShift
{
    public class UpdateShiftCommandHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateShiftCommand, ApiResponse<int>>
    {
        public async Task<ApiResponse<int>> Handle(UpdateShiftCommand request, CancellationToken cancellationToken)
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

            return ApiResponse<int>.SuccessResponse(shift.Id, $"Shift with Id: {shift.Id} successfully updated");
        }
    }
}
