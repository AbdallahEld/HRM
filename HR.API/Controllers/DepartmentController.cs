using HR.Application.Departments.DTOs;
using HR.Application.Departments.Queries.GetAllDepartments;
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
    }
}
