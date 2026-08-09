namespace HR.Application.Features.Attendance.Services
{
    public class ClockService : IClockService
    {
        public double CalculateDistance(decimal lat1, decimal lon1, decimal? lat2, decimal? lon2)
        {
            double dLat1 = (double)lat1;
            double dLon1 = (double)lon1;
            double dLat2 = (double)lat2;
            double dLon2 = (double)lon2;

            var r = 6371e3;
            var dLat = (dLat2 - dLat1) * (Math.PI / 180.0);
            var dLon = (dLon2 - dLon1) * (Math.PI / 180.0);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(dLat1 * (Math.PI / 180.0)) * Math.Cos(dLat2 * (Math.PI / 180.0)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return r * c;
        }
    }
}
