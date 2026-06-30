using MediatR;

namespace HR.Application.Features.Shift.Commands.DeleteShift
{
    public class DeleteShiftCommand (int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
