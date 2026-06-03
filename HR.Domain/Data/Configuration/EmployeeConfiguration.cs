using HR.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Domain.Data.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(e => e.LastName)
                   .IsRequired()
                   .HasMaxLength(50);
            
            builder.Property(e => e.Email)
                   .IsRequired()
                   .HasMaxLength(100);

             builder.Property(e => e.PhoneNumber)
                   .HasMaxLength(20);

             builder.Property(e => e.NationalId)
                   .HasMaxLength(20);

             builder.Property(e => e.Position)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(e => e.department)
                   .WithMany(e => e.Employees)
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
