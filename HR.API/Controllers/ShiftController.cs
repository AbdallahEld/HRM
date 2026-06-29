using HR.Application.Shared;
using HR.Application.Shift.DTOs;
using HR.Application.Shift.Querries.GetAllShifts;
using HR.Application.Shift.Querries.GetShiftById;
using HR.Domain.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
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
    }
}
