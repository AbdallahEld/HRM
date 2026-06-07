using HR.Domain.Data.Entities;
using HR.Domain.Repository;
using HR.Infrastructure.Persistance;

namespace HR.Infrastructure.Repository
{
    public class EmployeeDeductionsRepository : GenericRepository<EmployeeDeductions>, IEmployeeDeductionsRepository
    {
        public EmployeeDeductionsRepository(HRDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
