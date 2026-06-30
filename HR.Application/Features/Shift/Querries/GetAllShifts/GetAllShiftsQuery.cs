using HR.Application.Features.Shift.DTOs;
using MediatR;

namespace HR.Application.Features.Shift.Querries.GetAllShifts
{
    public class GetAllShiftsQuery : IRequest<IEnumerable<ShiftReadDTO>>
    {
    }
}
