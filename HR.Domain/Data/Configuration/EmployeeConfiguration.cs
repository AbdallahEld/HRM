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

            builder.Property(e => e.PhoneNumber)
                   .HasMaxLength(20);

            builder.Property(e => e.NationalId)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(e => e.Position)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.Nationality)
                   .HasMaxLength(50);

            builder.HasIndex(e => e.NationalId)
                   .IsUnique();

            builder.Property(e => e.BaseSalary)
                   .IsRequired()
                   .HasPrecision(18, 2);

            builder.Property(e => e.HourlyRate)
                   .IsRequired()
                   .HasPrecision(18, 2);

            builder.Property(e => e.Gender)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(e => e.EmploymentType)
                   .HasConversion<string>()
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(e => e.EmploymentStatus)
                   .HasConversion<string>()
                   .HasMaxLength(30)
                   .IsRequired();

            builder.HasOne(e => e.Department)
                   .WithMany(e => e.Employees)
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);

             builder.HasOne(e => e.Manager)
                    .WithMany(e => e.Subordinates)
                    .HasForeignKey(e => e.ManagerId)
                    .OnDelete(DeleteBehavior.Restrict);

             builder.HasMany(e => e.ApprovedLeaves)
                    .WithOne(l => l.Approver) 
                    .HasForeignKey(l => l.ApproverId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
