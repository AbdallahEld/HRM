using MediatR;

namespace HR.Application.Shift.Commands.DeleteShift
{
    public class DeleteShiftCommand (int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
