using HR.Domain.Data.Entities;
using HR.Domain.Repository;
using HR.Infrastructure.Persistance;

namespace HR.Infrastructure.Repository
{
    public class LocationShiftsRepository : GenericRepository<LocationShifts>, ILocationShiftsRepositroy
    {
        public LocationShiftsRepository(HRDbContext dbContext) : base(dbContext)
        {

        }
    }
}
