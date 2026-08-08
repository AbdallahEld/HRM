using HR.Application.Features.LocationShifts.Commands.AssignShiftToLocation;
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
