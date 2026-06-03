using HR.Domain.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Domain.Data.Entities
{
    public class Leave : BaseEntity
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public LeaveStatus Status { get; set; }

        //---------------------------One To Many Relationship----------------------------//
        [ForeignKey("Employee")]
        public int ApproverId { get; set; }
        [InverseProperty("ApprovedLeaves")]
        public Employee Approver { get; set; }
        //-------------------------------------------------------------------------------//
        public DateOnly ApprovedAt { get; set; }

        //---------------------------One To Many Relationship----------------------------//
        [ForeignKey("LeaveType")]
        public int LeaveTypeId { get; set; }
        [InverseProperty("Leaves")]
        public LeaveType LeaveType { get; set; }
        //-------------------------------------------------------------------------------//

        //---------------------------One To Many Relationship----------------------------//
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [InverseProperty("Leaves")]
        public Employee Employee { get; set; }
        //-------------------------------------------------------------------------------//
    }
}
