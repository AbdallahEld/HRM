using HR.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Domain.Data.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(d => d.CostCenter)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.HasIndex(d => d.Name)
                   .IsUnique();


            builder.HasOne(d => d.Manager)
                   .WithOne(e => e.ManagedDepartment)
                   .HasForeignKey<Department>(d => d.ManagerId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
