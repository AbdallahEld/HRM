namespace HR.Application.Shift.DTOs
{
    public class ShiftReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsFlexible { get; set; }
        public int? RequiredHours { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public int GracePeriodMinutes { get; set; }
    }
}
