using HR.Application.Features.Location.Commands.Shared;
using MediatR;

namespace HR.Application.Features.Location.Commands.UpdateLocation
{
    public class UpdateLocationCommand : IRequest<int>, ILocationCommand
    {
        public int Id { get; set; }
        public bool IsRemote { get; set; }
        public string? Address { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
    }
}
