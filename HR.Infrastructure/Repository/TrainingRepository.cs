using HR.Domain.Data.Entities;
using HR.Domain.Repository;
using HR.Infrastructure.Persistance;

namespace HR.Infrastructure.Repository
{
    public class TrainingRepository : GenericRepository<Training>, ITrainingRepository
    {
        public TrainingRepository(HRDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
