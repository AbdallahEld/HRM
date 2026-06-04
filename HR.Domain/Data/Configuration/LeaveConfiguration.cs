using HR.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Domain.Data.Configuration
{
    public class LeaveConfiguration : IEntityTypeConfiguration<Leave>
    {
        public void Configure(EntityTypeBuilder<Leave> builder)
        {
            builder.Property(l => l.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.HasOne(l => l.Employee)
                   .WithMany(e => e.Leaves)
                   .HasForeignKey(l => l.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.Approver)
                   .WithMany(e => e.ApprovedLeaves)
                   .HasForeignKey(l => l.ApproverId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.LeaveType)
                   .WithMany(lt => lt.Leaves)
                   .HasForeignKey(l => l.LeaveTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
