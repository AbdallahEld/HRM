using HR.Domain.Data.Entities;
using HR.Domain.Repository;
using HR.Infrastructure.Persistance;

namespace HR.Infrastructure.Repository
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(HRDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
