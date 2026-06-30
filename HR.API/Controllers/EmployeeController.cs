using HR.Application.Features.Employee.DTOs;
using HR.Application.Features.Employee.Queries.GetAllEmployees;
using HR.Application.Features.Employee.Queries.GetEmployeeById;
using HR.Application.Shared;
using MediatR;
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
    }
}
