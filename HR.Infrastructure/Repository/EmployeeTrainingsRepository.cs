using HR.Domain.Data.Entities;
using HR.Domain.Repository;
using HR.Infrastructure.Persistance;

namespace HR.Infrastructure.Repository
{
    public class EmployeeTrainingsRepository : GenericRepository<EmployeeTrainings>, IEmployeeTrainingsRepository
    {
        public EmployeeTrainingsRepository(HRDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
