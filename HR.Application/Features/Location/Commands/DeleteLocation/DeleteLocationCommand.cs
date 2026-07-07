using MediatR;

namespace HR.Application.Features.Location.Commands.DeleteLocation
{
    public class DeleteLocationCommand (int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
