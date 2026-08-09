namespace HR.Domain.Data.Entities
{
    public class EmployeeLeaveBalance : BaseEntity
    {
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public int Year { get; set; }

        public int TotalAllocatedDays { get; set; }
        public int UsedDays { get; set; }

        public Employee Employee { get; set; } = null!;
        public LeaveType LeaveType { get; set; } = null!;
    }
}
