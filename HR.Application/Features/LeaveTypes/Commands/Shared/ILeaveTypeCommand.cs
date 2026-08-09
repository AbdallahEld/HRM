namespace HR.Application.Features.LeaveTypes.Commands.Shared
{
    public interface ILeaveTypeCommand
    {
        public string Name { get; set; }
        public int MaxDaysPerYear { get; set; }
        public bool IsPaid { get; set; }
        public bool CarryOverAllowed { get; set; }
        public bool RequiresApproval { get; set; }
    }
}
