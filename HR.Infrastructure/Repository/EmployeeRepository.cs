using HR.Domain.Data.Entities;
using HR.Domain.Repository;
using HR.Infrastructure.Persistance;

namespace HR.Infrastructure.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(HRDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
