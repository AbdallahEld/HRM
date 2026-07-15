using HR.Application.Features.Location.Commands.Shared;
using HR.Application.Shared;
using MediatR;

namespace HR.Application.Features.Location.Commands.UpdateLocation
{
    public class UpdateLocationCommand : IRequest<ApiResponse<int>>, ILocationCommand
    {
        public int Id { get; set; }
        public bool IsRemote { get; set; }
        public string? Address { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
    }
}
