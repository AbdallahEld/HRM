using HR.Application.Features.Leaves.Commands.SubmitLeaveRequest;
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


        [HttpPost]
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
