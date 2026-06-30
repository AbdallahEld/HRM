using HR.Application.Features.Shift.Commands.Shared;
using MediatR;

namespace HR.Application.Features.Shift.Commands.UpdateShift
{
    public class UpdateShiftCommand : IRequest<int> , IShiftCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsFlexible { get; set; }
        public int? RequiredHours { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public int GracePeriodMinutes { get; set; }
    }
}
