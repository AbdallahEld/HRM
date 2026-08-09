using HR.Application.Features.LeaveTypes.Commands.CreateLeaveType;
using HR.Application.Features.LeaveTypes.Commands.DeleteLeaveType;
using HR.Application.Features.LeaveTypes.Commands.UpdateLeaveType;
using HR.Application.Features.LeaveTypes.Queries.GetAllLeaveTypes;
using HR.Application.Features.LeaveTypes.Queries.GetLeaveTypeById;
using HR.Application.Features.Shift.Commands.CreateShift;
using HR.Application.Features.Shift.Commands.DeleteShift;
using HR.Application.Features.Shift.Commands.UpdateShift;
using HR.Application.Features.Shift.DTOs;
using HR.Application.Features.Shift.Querries.GetAllShifts;
using HR.Application.Features.Shift.Querries.GetShiftById;
using HR.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypeController (
        IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] GetAllLeaveTypesQuery query)
        {
            var response = await mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var response = await mediator.Send(new GetLeaveTypeByIdQuery(id));
            
            if(!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "HRManager,SystemAdmin")]
        public async Task<IActionResult> Create([FromBody] CreateLeaveTypeCommand command)
        {
            var response = await mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "HRManager,SystemAdmin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateLeaveTypeCommand command)
        {
            command.Id = id;

            var response = await mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "HRManager,SystemAdmin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await mediator.Send(new DeleteLeaveTypeCommand(id));
            if (!response.Success)
            {
                return NotFound(response);
            }
            return NoContent();
        }
    }
}
