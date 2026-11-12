using HR.Application.Shared;
using HR.Domain.Data.Entities;

namespace HR.Application.Features.Leave.Services.ValidateNoOverlap
{
    public interface INoOverlapChecker
    {
        ApiResponse<string> ValidateNoOverlap(DateOnly startDate, DateOnly endDate, IReadOnlyList<Domain.Data.Entities.Leave> existingLeaves);
    }
}
