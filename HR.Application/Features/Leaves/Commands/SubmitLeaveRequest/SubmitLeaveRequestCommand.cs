using HR.Application.Shared;
using MediatR;
using System.Text.Json.Serialization;

namespace HR.Application.Features.Leaves.Commands.SubmitLeaveRequest
{
    public class SubmitLeaveRequestCommand : IRequest<ApiResponse<string>>
    {
        [JsonIgnore]
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Reason { get; set; }

    }
}
