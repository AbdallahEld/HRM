using HR.Application.Shared;
using MediatR;
using System.Text.Json.Serialization;

namespace HR.Application.Features.Attendance.Commands.ClockIn
{
    public class ClockInCommand : IRequest<ApiResponse<int>>
    {
        public int LocationId { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }

        [JsonIgnore]
        public int EmployeeId { get; set; }
    }
}
