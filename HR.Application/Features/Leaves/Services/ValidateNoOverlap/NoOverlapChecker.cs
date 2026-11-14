using HR.Application.Shared;

namespace HR.Application.Features.Leave.Services.ValidateNoOverlap
{
    public class NoOverlapChecker : INoOverlapChecker
    {
        public ApiResponse<string> ValidateNoOverlap(DateOnly startDate, DateOnly endDate, IReadOnlyList<Domain.Data.Entities.Leave> existingLeaves)
        {
            foreach (var leave in existingLeaves)
            {
                if (startDate <= leave.EndDate && endDate >= leave.StartDate)
                {
                    return ApiResponse<string>.FailureResponse(new List<string>
                    {
                        "The requested leave period overlaps with an existing leave."
                    });
                }
            }
            return ApiResponse<string>.SuccessResponse("No overlap detected.");
        }
    }
}
