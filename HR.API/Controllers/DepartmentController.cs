using HR.Application.Departments.Commands.CreateDepartment;
using HR.Application.Departments.Commands.DeleteDepartment;
using HR.Application.Departments.Commands.UpdateDepartment;
using HR.Application.Departments.DTOs;
using HR.Application.Departments.Queries.GetAllDepartments;
using HR.Application.Departments.Queries.GetDepartmentById;
using HR.Application.Shared;
using HR.Domain.Data.Entities;
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
        public async Task<ActionResult> GetAll([FromQuery] GetAllDepartmentsQuery query)
        {
            var departments = await mediator.Send(query);
            return Ok(ApiResponse<IEnumerable<DepartmentReadDTO>>.SuccessResponse(departments, "Departments retrieved successfully"));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById ([FromRoute] int id)
        {
            var department = await mediator.Send(new GetDepartmentByIdQuery(id));
            return Ok(ApiResponse<DepartmentReadDTO>.SuccessResponse(department, "Departments retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentCommand command)
        {
            var Id = await mediator.Send(command);
            return Ok(ApiResponse<int>.SuccessResponse(Id, $"Departments with Id: {Id} successfully created"));
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDepartmentCommand command)
        {
            command.Id = id;
            var Id = await mediator.Send(command);

            return Ok(ApiResponse<int>.SuccessResponse(Id, $"Departments with Id: {Id} successfully updated"));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) 
        {
            await mediator.Send(new DeleteDepartmentCommand(id)); 
            return NoContent();
        }
    }
}
