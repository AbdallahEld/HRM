using HR.Domain.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Domain.Data.Entities
{
    public class Attendance : BaseEntity
    {
        public DateOnly Date { get; set; }
        public TimeSpan TimeIn { get; set; }
        public TimeSpan TimeOut { get; set; }
        public AttendanceStatus Status { get; set; }
        public int OverTimeHours { get; set; } = 0;
        public AttendanceSource Source { get; set; }


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
    }
}
