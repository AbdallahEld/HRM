using HR.Application.Features.Attendance.Services;
using HR.Application.Shared;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.Attendance.Commands.ClockOut
{
    public class ClockOutCommandHandler (
        IUnitOfWork unitOfWork,
        IClockService clockService) : IRequestHandler<ClockOutCommand, ApiResponse<int>>
    {
        public async Task<ApiResponse<int>> Handle(ClockOutCommand request, CancellationToken cancellationToken)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var activeAttendance = await unitOfWork._AttendanceRepository.GetAsync(a => a.EmployeeId == request.EmployeeId 
            && today == a.Date
            && a.TimeOut == null);

            if (activeAttendance == null)
            {
                return ApiResponse<int>.FailureResponse(
                    new List<string> { "No active clock-in record found for today to clock out from." },
                    "Clock-Out failed");
            }

            var location = await unitOfWork._LocationRepository.GetByIdAsync(request.LocationId);
            if (location == null)
            {
                return ApiResponse<int>.FailureResponse(new List<string> { "Location not found." }, "Clock-Out failed");
            }

            if (!location.IsRemote)
            {
                var distanceInMeters = clockService.CalculateDistance(request.Lat, request.Long, location.Lat, location.Long);

                double maxAllowedDistance = 100;
                if (distanceInMeters > maxAllowedDistance)
                {
                    return ApiResponse<int>.FailureResponse(new List<string> { $"You are too far from the workplace. Distance: {Math.Round(distanceInMeters)}m." }, "Clock-Out failed");
                }
            }

            var shift = await unitOfWork._ShiftRepository.GetByIdAsync(activeAttendance.ShiftId);
            if (shift == null)
            {
                return ApiResponse<int>.FailureResponse(new List<string> { "Shift data not found." }, "Clock-Out failed");
            }

            var actualTimeOut = TimeOnly.FromDateTime(DateTime.UtcNow);
            activeAttendance.TimeOut = actualTimeOut;

            if(shift.EndTime.HasValue)
            {
                if (actualTimeOut < shift.EndTime.Value)
                {
                    var earlySpan = shift.EndTime.Value - actualTimeOut;
                    activeAttendance.EarlyDepartureMinutes = (int)earlySpan.TotalMinutes;
                }
                else if (actualTimeOut > shift.EndTime.Value)
                {
                    var overtimeSpan = actualTimeOut - shift.EndTime.Value;

                    if (overtimeSpan.TotalMinutes > shift.GracePeriodMinutes)
                    {
                        activeAttendance.OverTimeHours = (int)overtimeSpan.TotalHours;
                    }
                }
            }

            unitOfWork._AttendanceRepository.UpdateAsync(activeAttendance);
            await unitOfWork.SaveChangesAsync();

            return ApiResponse<int>.SuccessResponse(activeAttendance.Id, "Clocked-Out successfully");
        }
    }
}
