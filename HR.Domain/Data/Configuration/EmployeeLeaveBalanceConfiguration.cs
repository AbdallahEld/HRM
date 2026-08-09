using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HR.Domain.Data.Entities;

namespace HR.Infrastructure.Data.Configurations
{
    public class EmployeeLeaveBalanceConfiguration : IEntityTypeConfiguration<EmployeeLeaveBalance>
    {
        public void Configure(EntityTypeBuilder<EmployeeLeaveBalance> builder)
        {
            builder.ToTable("EmployeeLeaveBalances");

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => new { e.EmployeeId, e.LeaveTypeId, e.Year })
                   .IsUnique();

            builder.Property(e => e.TotalAllocatedDays)
                   .IsRequired();

            builder.Property(e => e.UsedDays)
                   .HasDefaultValue(0)
                   .IsRequired();

            builder.HasOne(e => e.Employee)
                   .WithMany()
                   .HasForeignKey(e => e.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.LeaveType)
                   .WithMany()
                   .HasForeignKey(e => e.LeaveTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
