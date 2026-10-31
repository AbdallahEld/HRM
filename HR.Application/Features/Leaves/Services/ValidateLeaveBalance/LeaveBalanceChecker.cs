using HR.Application.Shared;
using HR.Domain.UnitOfWork;

namespace HR.Application.Features.Leaves.Services.ValidateLeaveBalance
{
    public class LeaveBalanceChecker : ILeaveBalanceChecker
    {
        private readonly IUnitOfWork _unitOfWork;
        public LeaveBalanceChecker(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<string>> ValidateLeaveBalanceAsync(int leaveTypeId, int requestedDays, int employeeId)
        {
            var leaveBalance = await _unitOfWork._EmployeeLeaveBalanceRepository
                .GetAsync(b => b.EmployeeId == employeeId && b.LeaveTypeId == leaveTypeId);

            if (leaveBalance == null)
            {
                return ApiResponse<string>.FailureResponse(new List<string>
                {
                    "Leave balance not found for the specified leave type."
                });
            }

            var remainingBalance = leaveBalance.TotalAllocatedDays - leaveBalance.UsedDays;

            if (requestedDays > remainingBalance)
            {
                return ApiResponse<string>.FailureResponse(new List<string>
                {
                    $"Insufficient leave balance. You have {remainingBalance} days remaining for this leave type."
                });
            }

            return ApiResponse<string>.SuccessResponse("Leave balance is sufficient.");
        }
    }
}
