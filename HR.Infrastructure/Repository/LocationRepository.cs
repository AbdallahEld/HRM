using HR.Domain.Data.Entities;
using HR.Domain.Repository;
using HR.Infrastructure.Persistance;

namespace HR.Infrastructure.Repository
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(HRDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
