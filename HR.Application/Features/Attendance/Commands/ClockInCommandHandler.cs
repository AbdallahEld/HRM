using HR.Application.Shared;
using HR.Domain.Data.Entities.Enums;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.Attendance.Commands
{
    public class ClockInCommandHandler (
        IUnitOfWork unitOfWork) : IRequestHandler<ClockInCommand, ApiResponse<int>>
    {
        public async Task<ApiResponse<int>> Handle(ClockInCommand request, CancellationToken cancellationToken)
        {
            var employee = await unitOfWork._EmployeeRepository.GetByIdAsync(request.EmployeeId);
            if (employee == null || employee.DefaultShiftId == null)
            {
                return ApiResponse<int>.FailureResponse(new List<string> { "Employee or assigned shift not found." }, "Clock-In failed");
            }

            var location = await unitOfWork._LocationRepository.GetByIdAsync(request.LocationId);
            if (location == null)
            {
                return ApiResponse<int>.FailureResponse(new List<string> { "Location not found." }, "Clock-In failed");
            }

            var isShiftExistInLocation = await unitOfWork._LocationShiftsRepositroy.GetAsync(ls => ls.LocationId == request.LocationId && ls.ShiftId == employee.DefaultShiftId);
            if (isShiftExistInLocation == null)
            {
                return ApiResponse<int>.FailureResponse(new List<string> { "Your assigned shift is not available at this location." }, "Clock-In failed");
            }

            if (location.IsRemote)
            {
                if (!employee.CanWorkRemotely)
                {
                    return ApiResponse<int>.FailureResponse(new List<string> { "You do not have permission to work remotely." }, "Clock-In failed");
                }
            }
            else
            {
                var distanceInMeters = CalculateDistance(request.Lat, request.Long, location.Lat, location.Long);

                double maxAllowedDistance = 100;
                if (distanceInMeters > maxAllowedDistance)
                {
                    return ApiResponse<int>.FailureResponse(new List<string> { $"You are too far from the workplace. Distance: {Math.Round(distanceInMeters)}m." }, "Clock-In failed");
                }
            }

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var existingAttendance = await unitOfWork._AttendanceRepository.GetAsync(a => a.EmployeeId == request.EmployeeId && a.Date == today);
            if (existingAttendance != null)
            {
                return ApiResponse<int>.FailureResponse(new List<string> { "You have already clocked in today." }, "Clock-In failed");
            }

            var newAttendance = new Domain.Data.Entities.Attendance
            {
                EmployeeId = request.EmployeeId,
                LocationId = request.LocationId,
                ShiftId = employee.DefaultShiftId,
                Date = today,
                TimeIn = TimeOnly.FromDateTime(DateTime.UtcNow),
                Status = AttendanceStatus.Present,
                Source = AttendanceSource.App 
            };

            await unitOfWork._AttendanceRepository.AddAsync(newAttendance);
            await unitOfWork.SaveChangesAsync();

            return ApiResponse<int>.SuccessResponse(newAttendance.Id, "Clocked-In successfully");
        }

        private double CalculateDistance(decimal lat1, decimal lon1, decimal? lat2, decimal? lon2)
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
