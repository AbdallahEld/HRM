using HR.Application.Shared;
using HR.Domain.Data.Entities;

namespace HR.Application.Features.Leaves.Services.ValidateNoOverlap
{
    public interface INoOverlapChecker
    {
        ApiResponse<string> ValidateNoOverlap(DateOnly startDate, DateOnly endDate, IReadOnlyList<Leave> existingLeaves);
    }
}
