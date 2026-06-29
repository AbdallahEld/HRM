using HR.Application.Shift.DTOs;
using MediatR;

namespace HR.Application.Shift.Querries.GetAllShifts
{
    public class GetAllShiftsQuery : IRequest<IEnumerable<ShiftReadDTO>>
    {
    }
}
