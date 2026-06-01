using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Domain.Entities
{
    public class Attendance : BaseEntity
    {
        public DateTime Date { get; set; }
        public TimeSpan TimeIn { get; set; }
        public TimeSpan TimeOut { get; set; }

        //---------------------------One To Many Relationship----------------------------//
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [InverseProperty("Attendances")]
        public Employee Employee { get; set; }
        //-------------------------------------------------------------------------------//
    }
}
