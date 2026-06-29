using HR.Application.Shift.DTOs;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Shift.Querries.GetAllShifts
{
    public class GetAllShiftsQueryHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<GetAllShiftsQuery, IEnumerable<ShiftReadDTO>>
    {
        public async Task<IEnumerable<ShiftReadDTO>> Handle(GetAllShiftsQuery request, CancellationToken cancellationToken)
        {
            var shifts = await unitOfWork._ShiftRepository.GetAllAsync();

            var shiftsReadDTOs = shifts.Select(s => new ShiftReadDTO
            {
                Id = s.Id,
                Name = s.Name,
                IsFlexible = s.IsFlexible,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                GracePeriodMinutes = s.GracePeriodMinutes,
                RequiredHours = s.RequiredHours,
            });

            return shiftsReadDTOs;
        }
    }
}

