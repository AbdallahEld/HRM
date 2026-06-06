using HR.Domain.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Domain.Data.Entities
{
    public class Attendance : BaseEntity
    {
        public DateOnly Date { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public AttendanceStatus Status { get; set; }
        public AttendanceSource Source { get; set; }
        public int LateMinutes { get; set; } = 0;
        public int EarlyDepartureMinutes { get; set; } = 0;
        public int OverTimeHours { get; set; } = 0;


        //---------------------------One To Many Relationship----------------------------//
        [ForeignKey("Location")]
        public int LocationId { get; set; }
        [InverseProperty("Attendances")]
        public Location Location { get; set; }
        //-------------------------------------------------------------------------------//

        //---------------------------One To Many Relationship----------------------------//
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [InverseProperty("Attendances")]
        public Employee Employee { get; set; }
        //-------------------------------------------------------------------------------//

        //---------------------------One To Many Relationship----------------------------//
        [ForeignKey("Shift")]
        public int ShiftId { get; set; }
        [InverseProperty("Attendances")]
        public Shift Shift { get; set; }
        //-------------------------------------------------------------------------------//
    }
}
