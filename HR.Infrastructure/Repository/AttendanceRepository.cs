using HR.Domain.Data.Entities;
using HR.Domain.Repository;
using HR.Infrastructure.Persistance;

namespace HR.Infrastructure.Repository
{
    public class AttendanceRepository : GenericRepository<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(HRDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
