using HR.Application.Shared;
using HR.Domain.Data.Entities.Enums;

namespace HR.Application.Features.Leaves.Services.ValidatePastDate
{
    public class PastDateChecker : IPastDateChecker
    {
        public ApiResponse<string> ValidatePastDate(DateOnly startDate, string leaveStatus)
        {
            if (startDate < DateOnly.FromDateTime(DateTime.Now) && leaveStatus != "Sick Leave")
            {
                return ApiResponse<string>.FailureResponse(new List<string>
                {
                    "The requested leave start date is in the past and cannot be approved."
                });
            }
            return ApiResponse<string>.SuccessResponse("Leave start date is valid.");
        }
    }
}
