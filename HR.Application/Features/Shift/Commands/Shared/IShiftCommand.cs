using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Features.Shift.Commands.Shared
{
    public interface IShiftCommand
    {
        string Name { get; }
        bool IsFlexible { get; }
        int? RequiredHours { get; }
        TimeOnly? StartTime { get; }
        TimeOnly? EndTime { get; }
        int GracePeriodMinutes { get; }
    }
}
