using HR.Application.Shared;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.Shift.Commands.CreateShift
{
    public class CreateShiftCommandHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<CreateShiftCommand, ApiResponse<int>>
    {
        public async Task<ApiResponse<int>> Handle(CreateShiftCommand request, CancellationToken cancellationToken)
        {
            var shift = new Domain.Data.Entities.Shift
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
            return ApiResponse<int>.SuccessResponse(shift.Id, "Shift created successfully");
        }
    }
}
