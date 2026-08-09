namespace HR.Application.Features.LeaveTypes.DTOs
{
    public class LeaveTypeReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxDaysPerYear { get; set; }
        public bool IsPaid { get; set; }
        public bool CarryOverAllowed { get; set; }
        public bool RequiresApproval { get; set; }
    }
}
