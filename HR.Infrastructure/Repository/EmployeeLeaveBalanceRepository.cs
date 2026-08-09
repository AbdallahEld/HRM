using HR.Domain.Data.Entities;
using HR.Domain.Repository;
using HR.Infrastructure.Persistance;

namespace HR.Infrastructure.Repository
{
    public class EmployeeLeaveBalanceRepository : GenericRepository<EmployeeLeaveBalance>, IEmployeeLeaveBalanceRepository
    {
        public EmployeeLeaveBalanceRepository(HRDbContext dbContext):base(dbContext)
        {
            
        }
    }
}
