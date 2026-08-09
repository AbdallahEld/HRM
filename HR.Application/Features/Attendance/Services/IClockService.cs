namespace HR.Application.Features.Attendance.Services
{
    public interface IClockService
    {
        double CalculateDistance(decimal lat1, decimal lon1, decimal? lat2, decimal? lon2);
    }
}
