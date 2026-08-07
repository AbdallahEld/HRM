using HR.Application.Features.Departments.Commands.CreateDepartment;
using HR.Application.Features.Departments.Commands.DeleteDepartment;
using HR.Application.Features.Departments.Commands.UpdateDepartment;
using HR.Application.Features.Departments.DTOs;
using HR.Application.Features.Departments.Queries.GetAllDepartments;
using HR.Application.Features.Departments.Queries.GetDepartmentById;
using HR.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController (
        IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "HRManager,SystemAdmin")]
        public async Task<ActionResult> GetAll([FromQuery] GetAllDepartmentsQuery query)
        {
            var response = await mediator.Send(query);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "HRManager,SystemAdmin")]
        public async Task<ActionResult> GetById ([FromRoute] int id)
        {
            var response = await mediator.Send(new GetDepartmentByIdQuery(id));
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentCommand command)
        {
            var response = await mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDepartmentCommand command)
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
        [Authorize(Roles = "SystemAdmin")]
        public async Task<IActionResult> Delete([FromRoute] int id) 
        {
            await mediator.Send(new DeleteDepartmentCommand(id)); 
            return NoContent();
        }
    }
}
