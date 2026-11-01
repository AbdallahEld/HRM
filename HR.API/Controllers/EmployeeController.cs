using HR.Application.Features.Employee.Commands.PromoteToDepartmentManager;
using HR.Application.Features.Employee.DTOs;
using HR.Application.Features.Employee.Queries.GetAllEmployees;
using HR.Application.Features.Employee.Queries.GetEmployeeById;
using HR.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController (
        IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] GetAllEmployeesQuery query)
        {
            var employees = await mediator.Send(query);
            return Ok(ApiResponse<IEnumerable<EmployeeReadDTO>>.SuccessResponse(employees, "Employees retrieved successfully"));
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var employee = await mediator.Send(new GetEmployeeByIdQuery(id));
            return Ok(ApiResponse<EmployeeReadDTO>.SuccessResponse(employee, "Employee retrieved successfully"));
        }

        [HttpPost("promote-to-department-manager")]
        [Authorize(Roles = "HRManager,SystemAdmin")]
        public async Task<ActionResult> PromoteToDepartmentManager([FromBody] PromoteToDepartmentManagerCommand command)
        {
            var result = await mediator.Send(command);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
