using HR.Application.Features.Departments.Commands.CreateDepartment;
using HR.Application.Features.Departments.Commands.DeleteDepartment;
using HR.Application.Features.Departments.Commands.UpdateDepartment;
using HR.Application.Features.Employee.DTOs;
using HR.Application.Features.Employee.Queries.GetAllEmployees;
using HR.Application.Features.Employee.Queries.GetEmployeeById;
using HR.Application.Features.Location.Commands.CreateLocation;
using HR.Application.Features.Location.Commands.DeleteLocation;
using HR.Application.Features.Location.Commands.UpdateLocation;
using HR.Application.Features.Location.DTOs;
using HR.Application.Features.Location.Queries.GetAllLocations;
using HR.Application.Features.Location.Queries.GetLocationById;
using HR.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController (
        IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] GetAllLocationsQuery query)
        {
            var locations = await mediator.Send(query);
            return Ok(ApiResponse<IEnumerable<LocationReadDTO>>.SuccessResponse(locations, "Locations retrieved successfully"));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var location = await mediator.Send(new GetLocationByIdQuery(id));
            return Ok(ApiResponse<LocationReadDTO>.SuccessResponse(location, "Location retrieved successfully"));
        }

        [HttpPost]
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
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateLocationCommand command)
        {
            command.Id = id;
            var Id = await mediator.Send(command);

            return Ok(ApiResponse<int>.SuccessResponse(Id, $"Departments with Id: {Id} successfully updated"));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await mediator.Send(new DeleteLocationCommand(id));
            return NoContent();
        }
    }
}
