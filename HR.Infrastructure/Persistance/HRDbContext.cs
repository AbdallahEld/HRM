using HR.Domain.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Persistance
{
    public class HRDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
