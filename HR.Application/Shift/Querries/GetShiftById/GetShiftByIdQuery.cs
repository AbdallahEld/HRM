using HR.Application.Shift.DTOs;
using MediatR;

namespace HR.Application.Shift.Querries.GetShiftById
{
    public class GetShiftByIdQuery (int id) : IRequest<ShiftReadDTO>
    {
        public int Id { get; } = id;
    }
}
