using HR.Domain.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Domain.Data.Entities
{
    public class Payroll : BaseEntity
    {
        public decimal Salary { get; set; }
        public decimal NetPay { get; set; }
        public decimal GrossPay { get; set; }
        public decimal TaxDeduction { get; set; } = 0.00M;
        public decimal OtherDeductions { get; set; } = 0.00M;
        public DateOnly PayPeriodStart { get; set; } 
        public DateOnly PayPeriodEnd { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string Currency {  get; set; }

        //---------------------------One To Many Relationship-----------------------------//
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [InverseProperty("Payrolls")]
        public Employee Employee { get; set; }
        //-------------------------------------------------------------------------------//
    }
}
