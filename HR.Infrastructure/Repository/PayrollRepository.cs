using HR.Domain.Data.Entities;
using HR.Domain.Repository;
using HR.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Infrastructure.Repository
{
    public class PayrollRepository : GenericRepository<Payroll>, IPayrollRepository
    {
        public PayrollRepository(HRDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
