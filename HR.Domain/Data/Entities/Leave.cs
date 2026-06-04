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
        public int ApproverId { get; set; }
        public Employee Approver { get; set; }
        //-------------------------------------------------------------------------------//
        public DateOnly? ApprovedAt { get; set; }

        //---------------------------One To Many Relationship----------------------------//
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }
        //-------------------------------------------------------------------------------//

        //---------------------------One To Many Relationship----------------------------//
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        //-------------------------------------------------------------------------------//
    }
}
