using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Domain.Entities
{
    public class Payroll : BaseEntity
    {
        public decimal Salary { get; set; }
        public decimal NetPay { get; set; }

        //---------------------------One To Many Relationship-----------------------------//
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [InverseProperty("Payrolls")]
        public Employee Employee { get; set; }
        //-------------------------------------------------------------------------------//
    }
}
