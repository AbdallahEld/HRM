using HR.Domain.Repository;
using HR.Domain.UnitOfWork;
using HR.Infrastructure.Persistance;

namespace HR.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
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

        private readonly HRDbContext _dbContext;
        public UnitOfWork(
            IAttendanceRepository attendanceRepository,
            IDepartmentRepository departmentRepository,
            IEmployeeDeductionsRepository employeeDeductionsRepository,
            IEmployeeRepository employeeRepository,
            IEmployeeTrainingsRepository employeeTrainingsRepository,
            ILeaveRepository leaveRepository,
            ILeaveTypeRepository leaveTypeRepository,
            ILocationRepository locationRepository,
            ILocationShiftsRepositroy locationShiftsRepositroy,
            IPayrollRepository payrollRepository,
            IShiftRepository shiftRepository,
            ITrainingRepository trainingRepository,
            HRDbContext dbContext)
        {
            _AttendanceRepository = attendanceRepository;
            _DepartmentRepository = departmentRepository;
            _EmployeeDeductionsRepository = employeeDeductionsRepository;
            _EmployeeRepository = employeeRepository;
            _EmployeeTrainingsRepository = employeeTrainingsRepository;
            _LeaveRepository = leaveRepository;
            _LeaveTypeRepository = leaveTypeRepository;
            _LocationRepository = locationRepository;
            _LocationShiftsRepositroy = locationShiftsRepositroy;
            _PayrollRepository = payrollRepository;
            _ShiftRepository = shiftRepository;
            _TrainingRepository = trainingRepository;
            _dbContext = dbContext;
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
