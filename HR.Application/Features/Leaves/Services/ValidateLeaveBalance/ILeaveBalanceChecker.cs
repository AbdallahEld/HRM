using HR.Application.Shared;

namespace HR.Application.Features.Leave.Services.ValidateLeaveBalance
{
    public interface ILeaveBalanceChecker
    {
        public Task<ApiResponse<string>> ValidateLeaveBalanceAsync(int leaveTypeId, int requestedDays, int employeeId);
    }
}
