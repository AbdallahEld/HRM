using HR.Application.Features.Shift.Commands.CreateShift;
using HR.Application.Features.Shift.Commands.DeleteShift;
using HR.Application.Features.Shift.Commands.UpdateShift;
using HR.Application.Features.Shift.DTOs;
using HR.Application.Features.Shift.Querries.GetAllShifts;
using HR.Application.Features.Shift.Querries.GetShiftById;
using HR.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController (
        IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll ([FromQuery] GetAllShiftsQuery query)
        {
            var shifts = await mediator.Send(query);
            return Ok(ApiResponse<IEnumerable<ShiftReadDTO>>.SuccessResponse(shifts, "Shifts retrieved successfully"));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var shift = await mediator.Send(new GetShiftByIdQuery(id));
            return Ok(ApiResponse<ShiftReadDTO>.SuccessResponse(shift, "Shift retrieved successfully"));
        }

        [HttpPost]
        [Authorize(Roles = "HRManager,SystemAdmin")]
        public async Task<IActionResult> Create([FromBody] CreateShiftCommand command)
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
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateShiftCommand command)
        {
            command.Id = id;

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
            await mediator.Send(new DeleteShiftCommand(id));
            return NoContent();
        }
    }
}
