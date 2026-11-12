using HR.Application.Features.Leave.Commands.SubmitLeaveRequest;
using HR.Application.Features.Leave.Queries.GetDepartmentLeaveRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController (
        IMediator mediator): ControllerBase
    {
        [HttpGet("getDepartmentEmployeeLeaves")]
        [Authorize(Roles = "DepartmentHead")]
        public async Task<IActionResult> GetDepartmentEmployeeLeaveRequests([FromQuery] GetDepartmentLeaveRequestsQuery query)
        {
            var departmentHeadIdClaim = User.FindFirst("EmployeeId")?.Value;
            if (departmentHeadIdClaim == null)
            {
                return Unauthorized(new { Message = "Invalid Token: Deparment Manager With ID not found." });
            }

            query.ManagerId = int.Parse(departmentHeadIdClaim);
            
            var response = await mediator.Send(query);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        [HttpPost("SubmitLeaveRequest")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> SubmitLeaveRequest([FromBody] SubmitLeaveRequestCommand command)
        {
            var employeeIdClaim = User.FindFirst("EmployeeId")?.Value;
            if (employeeIdClaim == null)
            {
                return Unauthorized(new { Message = "Invalid Token: Employee ID not found." });
            }

            command.EmployeeId = int.Parse(employeeIdClaim);

            var response = await mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
