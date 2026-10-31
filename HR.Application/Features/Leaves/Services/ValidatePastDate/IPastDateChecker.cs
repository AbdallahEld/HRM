using HR.Application.Shared;
using HR.Domain.Data.Entities.Enums;

namespace HR.Application.Features.Leaves.Services.ValidatePastDate
{
    public interface IPastDateChecker
    {
        public ApiResponse<string> ValidatePastDate(DateOnly startDate, string leaveStatus);
    }
}
