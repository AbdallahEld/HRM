using HR.Domain.Data.Entities;
using HR.Domain.Repository;
using HR.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Infrastructure.Repository
{
    public class LocationShiftsRepository : GenericRepository<LocationShifts>, ILocationShiftsRepositroy
    {
        public LocationShiftsRepository(HRDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
