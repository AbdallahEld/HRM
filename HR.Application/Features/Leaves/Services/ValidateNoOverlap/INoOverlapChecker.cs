using HR.Application.Shared;

namespace HR.Application.Features.Leave.Services.ValidateNoOverlap
{
    public interface INoOverlapChecker
    {
        ApiResponse<string> ValidateNoOverlap(DateOnly startDate, DateOnly endDate, IReadOnlyList<Domain.Data.Entities.Leave> existingLeaves);
    }
}
