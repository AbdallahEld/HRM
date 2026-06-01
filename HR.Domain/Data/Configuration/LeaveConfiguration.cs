using HR.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Domain.Data.Configuration
{
    public class LeaveConfiguration : IEntityTypeConfiguration<Leave>
    {
        public void Configure(EntityTypeBuilder<Leave> builder)
        {
            builder.HasOne(l => l.Employee)
                   .WithMany(e => e.Leaves)
                   .HasForeignKey(l => l.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
