using HR.Domain.Repository;
using HR.Domain.UnitOfWork;
using HR.Infrastructure.Persistance;

namespace HR.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAttendanceRepository _AttendanceRepository { get; }
        public IDepartmentRepository _DepartmentRepository { get; }

        private readonly HRDbContext _dbContext;
        public UnitOfWork(
            IAttendanceRepository attendanceRepository,
            IDepartmentRepository departmentRepository,
            HRDbContext dbContext)
        {
            _AttendanceRepository = attendanceRepository;
            _DepartmentRepository = departmentRepository;
            _dbContext = dbContext;
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
