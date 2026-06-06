using HR.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Domain.Data.Configuration
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.Property(a => a.Date)
               .IsRequired();

            builder.Property(a => a.TimeIn)
                   .IsRequired(false);

            builder.Property(a => a.TimeOut)
                   .IsRequired(false);

            builder.Property(a => a.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(a => a.Source)
                   .HasConversion<string>()
                   .HasMaxLength(30)
                   .IsRequired();

            builder.HasIndex(a => new { a.EmployeeId, a.Date })
                   .IsUnique();

            builder.HasOne(a => a.Employee)
                   .WithMany(e => e.Attendances)
                   .HasForeignKey(a => a.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Location)
                   .WithMany(a => a.Attendances)
                   .HasForeignKey(a => a.LocationId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Shift)
                   .WithMany(s => s.Attendances)
                   .HasForeignKey(a => a.ShiftId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
