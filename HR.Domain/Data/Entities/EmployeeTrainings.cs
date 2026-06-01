using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Domain.Data.Entities
{
    public class EmployeeTrainings
    {
        //---------------------------One To Many Relationship----------------------------//
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [InverseProperty("EmployeeTrainings")]
        public Employee Employee { get; set; }
        //-------------------------------------------------------------------------------//

        //---------------------------One To Many Relationship----------------------------//
        [ForeignKey("Training")]
        public int TrainingId { get; set; }
        [InverseProperty("EmployeeTrainings")]
        public Training Training { get; set; }
        //-------------------------------------------------------------------------------//
    }
}
