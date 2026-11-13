using HR.Domain.Events.Employee;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.EmployeeLeaveBalance.Handlers
{
    public class DeductLeaveBalanceHandler (
        IUnitOfWork unitOfWork) : INotificationHandler<EmployeeLeaveApproved>
    {
        public async Task Handle(EmployeeLeaveApproved notification, CancellationToken cancellationToken)
        {
            var leave = notification.EmployeeLeave;

            var employeeLeaveBalance = await unitOfWork._EmployeeLeaveBalanceRepository.GetAsync(lb => lb.EmployeeId == leave.EmployeeId && lb.LeaveTypeId == leave.LeaveTypeId);
            if (employeeLeaveBalance == null)
            {
                // Handle the case where the leave balance record does not exist
                throw new Exception("Employee leave balance record not found.");
            }

            employeeLeaveBalance.UsedDays += leave.TotalDays;
            unitOfWork._EmployeeLeaveBalanceRepository.UpdateAsync(employeeLeaveBalance);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
