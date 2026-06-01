using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Domain.Entities
{
    public class Leave : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //---------------------------One To Many Relationship----------------------------//
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [InverseProperty("Leaves")]
        public Employee Employee { get; set; }
        //-------------------------------------------------------------------------------//
    }
}
