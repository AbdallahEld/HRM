using Microsoft.EntityFrameworkCore;

namespace HR.Infrastructure.Persistance
{
    public class HRDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
