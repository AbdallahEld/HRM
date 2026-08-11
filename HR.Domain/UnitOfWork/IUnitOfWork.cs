using HR.Domain.Repository;

namespace HR.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IAttendanceRepository _AttendanceRepository { get; }
        public IDepartmentRepository _DepartmentRepository { get; }
        public IEmployeeDeductionsRepository _EmployeeDeductionsRepository { get; }
        public IEmployeeRepository _EmployeeRepository { get; }
        public IEmployeeTrainingsRepository _EmployeeTrainingsRepository { get; }
        public ILeaveRepository _LeaveRepository { get; }
        public ILeaveTypeRepository _LeaveTypeRepository { get; }
        public ILocationRepository _LocationRepository { get; }
        public ILocationShiftsRepositroy _LocationShiftsRepositroy { get; }
        public IPayrollRepository _PayrollRepository { get; }
        public IShiftRepository _ShiftRepository { get; }
        public ITrainingRepository _TrainingRepository { get; }
        public IEmployeeLeaveBalanceRepository _EmployeeLeaveBalanceRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
