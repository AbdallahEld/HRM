using HR.Domain.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Domain.Data.Entities
{
    public class EmployeeDeductions : BaseEntity
    {
        public DateTime ActionDate { get; set; }
        public DeductionUnit Unit { get; set; }
        public decimal Quantity { get; set; }
        public decimal CalculatedAmount { get; set; }
        public string Reason { get; set; }
        public bool IsAppliedToPayroll { get; set; } = false;

        //---------------------------One To Many Relationship----------------------------//
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        //-------------------------------------------------------------------------------//
    }
}
