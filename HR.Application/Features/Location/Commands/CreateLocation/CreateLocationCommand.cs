using HR.Application.Features.Location.Commands.Shared;
using HR.Application.Shared;
using MediatR;

namespace HR.Application.Features.Location.Commands.CreateLocation
{
    public class CreateLocationCommand : IRequest<ApiResponse<int>>, ILocationCommand
    {
        public bool IsRemote { get; set; } = false;
        public string? Address { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
    }
}
