using HR.Domain.Data.Entities.Enums;

namespace HR.Application.Features.Leave.DTOs
{
    public class LeavesReadDTO 
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string LeaveType { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public LeaveStatus Status { get; set; }
        public int TotalWorkingDays { get; set; }
    }
}
