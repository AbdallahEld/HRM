using HR.Domain.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Domain.Data.Entities
{
    public class Attendance : BaseEntity
    {
        public DateOnly Date { get; set; }
        public TimeOnly? TimeIn { get; set; }
        public TimeOnly? TimeOut { get; set; }
        public AttendanceStatus Status { get; set; }
        public AttendanceSource Source { get; set; }
        public int LateMinutes { get; set; } = 0;
        public int EarlyDepartureMinutes { get; set; } = 0;
        public int OverTimeHours { get; set; } = 0;

        //---------------------------One To Many Relationship----------------------------//
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [InverseProperty("Attendances")]
        public Employee Employee { get; set; }
        //-------------------------------------------------------------------------------//

        //---------------------------One To Many Relationship----------------------------//
        public int ShiftId { get; set; }
        public int LocationId { get; set; }

        public LocationShifts LocationShift { get; set; }
        //-------------------------------------------------------------------------------//
    }
}
