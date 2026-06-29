using HR.Application.Shift.DTOs;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Shift.Querries.GetShiftById
{
    public class GetShiftByIdQueryHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<GetShiftByIdQuery, ShiftReadDTO>
    {
        public async Task<ShiftReadDTO> Handle(GetShiftByIdQuery request, CancellationToken cancellationToken)
        {
            var shift = await unitOfWork._ShiftRepository.GetByIdAsync(request.Id);
            if(shift == null)
            {
                throw new Exception($"Shift with Id {request.Id} not found.");
            }

            var shiftReadDTO = new ShiftReadDTO
            {
                Id = shift.Id,
                Name = shift.Name,
                IsFlexible = shift.IsFlexible,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
                GracePeriodMinutes = shift.GracePeriodMinutes,
                RequiredHours = shift.RequiredHours,
            };

            return shiftReadDTO;
        }
    }
}
