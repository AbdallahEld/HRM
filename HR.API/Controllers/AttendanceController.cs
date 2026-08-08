using HR.Application.Features.Attendance.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController (
        IMediator mediator) : ControllerBase
    {
        [HttpPost("clock-in")]
        [Authorize(Roles = "Employee")] // لازم يكون عامل لوجين
        public async Task<IActionResult> ClockIn([FromBody] ClockInCommand command)
        {
            var employeeIdClaim = User.FindFirst("EmployeeId")?.Value;

            if (string.IsNullOrEmpty(employeeIdClaim))
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
