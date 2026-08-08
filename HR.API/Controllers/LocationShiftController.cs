using HR.Application.Features.LocationShifts.Commands.AssignShiftToLocation;
using HR.Application.Features.LocationShifts.Queries.GetLocationAllShifts;
using HR.Application.Features.LocationShifts.Queries.GetShiftAllLocations;
using HR.Application.Features.Shift.Querries.GetAllShifts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationShiftController (
        IMediator mediator) : ControllerBase
    {
        [HttpGet("location/{locationId:int}")]
        [Authorize]
        public async Task<IActionResult> GetLocationShifts([FromRoute] int locationId)
        {
            var response = await mediator.Send(new GetLocationAllShiftsQuery(locationId));
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("shift/{shiftId:int}")]
        [Authorize]
        public async Task<IActionResult> GetShiftLocations([FromRoute] int shiftId)
        {
            var response = await mediator.Send(new GetShiftAllLocationsQuery(shiftId));
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPost]
        [Authorize(Roles = "HRManager,SystemAdmin")]
        public async Task<IActionResult> CreateLocationShift([FromBody] AssignShiftToLocationCommand command)
        {
            var response = await mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
