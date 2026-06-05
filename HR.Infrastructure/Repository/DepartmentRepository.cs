using HR.Domain.Data.Entities;
using HR.Domain.Repository;
using HR.Infrastructure.Persistance;

namespace HR.Infrastructure.Repository
{
    public class DepartmentRepository : GenericRepository<Department> ,IDepartmentRepository
    {
        public DepartmentRepository(HRDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
