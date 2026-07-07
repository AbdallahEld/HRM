using HR.Application.Features.Location.DTOs;
using MediatR;

namespace HR.Application.Features.Location.Queries.GetLocationById
{
    public class GetLocationByIdQuery (int id) : IRequest<LocationReadDTO>
    {
        public int Id { get; } = id;
    }
}
