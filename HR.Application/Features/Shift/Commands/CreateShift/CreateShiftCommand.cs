using HR.Application.Features.Shift.Commands.Shared;
using HR.Application.Shared;
using MediatR;

namespace HR.Application.Features.Shift.Commands.CreateShift
{
    public class CreateShiftCommand : IRequest<ApiResponse<int>>, IShiftCommand
    {
        public string Name { get; set; }
        public bool IsFlexible { get; set; }
        public int? RequiredHours { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public int GracePeriodMinutes { get; set; }
    }
}
