using HR.Application.Departments.Commands.CreateDepartment;
using HR.Application.Departments.Commands.DeleteDepartment;
using HR.Application.Departments.Commands.UpdateDepartment;
using HR.Application.Departments.DTOs;
using HR.Application.Departments.Queries.GetAllDepartments;
using HR.Application.Departments.Queries.GetDepartmentById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController (
        IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentReadDTO>>> GetAll([FromQuery] GetAllDepartmentsQuery query)
        {
            var departments = await mediator.Send(query);
            return Ok(departments);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DepartmentReadDTO>> GetById ([FromRoute] int id)
        {
            var department = await mediator.Send(new GetDepartmentByIdQuery(id));
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentCommand command)
        {
            var Id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { Id }, null);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDepartmentCommand command)
        {
            command.Id = id;
            var Id = await mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { Id }, null);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) 
        {
            await mediator.Send(new DeleteDepartmentCommand(id)); 
            return NoContent();
        }
    }
}
