using HR.Domain.Data.Entities;
using HR.Domain.Data.Entities.Enums;
using HR.Domain.Events.Employee;
using HR.Domain.UnitOfWork;
using MediatR;

namespace HR.Application.Features.Account.Handlers
{
    public class LeaveAllocationHandler (IUnitOfWork unitOfWork) : INotificationHandler<EmployeeRegistered>
    {
        public async Task Handle(EmployeeRegistered notification, CancellationToken cancellationToken)
        {
            var employee = notification.Employee;

            var leaveTypes = await unitOfWork._LeaveTypeRepository.GetAllAsync();
            var remainingMonths = 12 - employee.HireDate.Month + 1;

            foreach (var leaveType in leaveTypes)
            {
                int allocatedDays = leaveType.MaxDaysPerYear;

                if (leaveType.Name == "Maternity Leave" && employee.Gender != Gender.Female)
                {
                    continue;
                }

                if (leaveType.Name == "Annual Leave")
                {
                    double proportion = ((double)remainingMonths / 12.0) * leaveType.MaxDaysPerYear;
                    allocatedDays = (int)Math.Round(proportion);
                }

                var newBalance = new EmployeeLeaveBalance
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveType.Id,
                    Year = employee.HireDate.Year,
                    TotalAllocatedDays = allocatedDays,
                    UsedDays = 0
                };

                await unitOfWork._EmployeeLeaveBalanceRepository.AddAsync(newBalance);
            }

            await unitOfWork.SaveChangesAsync();
        }
    }
}
