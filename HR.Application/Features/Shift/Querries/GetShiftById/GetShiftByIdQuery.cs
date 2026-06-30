using HR.Application.Features.Shift.DTOs;
using MediatR;

namespace HR.Application.Features.Shift.Querries.GetShiftById
{
    public class GetShiftByIdQuery (int id) : IRequest<ShiftReadDTO>
    {
        public int Id { get; } = id;
    }
}
