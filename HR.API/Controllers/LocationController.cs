using HR.Application.Features.Location.Commands.CreateLocation;
using HR.Application.Features.Location.Commands.DeleteLocation;
using HR.Application.Features.Location.Commands.UpdateLocation;
using HR.Application.Features.Location.DTOs;
using HR.Application.Features.Location.Queries.GetAllLocations;
using HR.Application.Features.Location.Queries.GetLocationById;
using HR.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController (
        IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "HRManager,SystemAdmin")]
        public async Task<ActionResult> GetAll([FromQuery] GetAllLocationsQuery query)
        {
            var locations = await mediator.Send(query);
            return Ok(ApiResponse<IEnumerable<LocationReadDTO>>.SuccessResponse(locations, "Locations retrieved successfully"));
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "HRManager,SystemAdmin")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var location = await mediator.Send(new GetLocationByIdQuery(id));
            return Ok(ApiResponse<LocationReadDTO>.SuccessResponse(location, "Location retrieved successfully"));
        }

        [HttpPost]
        [Authorize(Roles = "HRManager,SystemAdmin")]
        public async Task<IActionResult> Create([FromBody] CreateLocationCommand command)
        {
            var response = await mediator.Send(command);
            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "HRManager,SystemAdmin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateLocationCommand command)
        {
            if (command.Id != id)
            {
                return BadRequest(ApiResponse<int>.FailureResponse(new List<string> { "Id in the route does not match Id in the body" }, "Invalid request"));
            }

            var response = await mediator.Send(command);
            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "HRManager,SystemAdmin")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await mediator.Send(new DeleteLocationCommand(id));
            return NoContent();
        }
    }
}
