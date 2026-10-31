using HR.Application.Shared;
using HR.Domain.Data.Entities;
using HR.Domain.UnitOfWork;

namespace HR.Application.Features.Leaves.Services.ValidateNoOverlap
{
    public class NoOverlapChecker : INoOverlapChecker
    {
        private readonly IUnitOfWork _unitOfWork;

        public NoOverlapChecker(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApiResponse<string> ValidateNoOverlap(DateOnly startDate, DateOnly endDate, IReadOnlyList<Leave> existingLeaves)
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
