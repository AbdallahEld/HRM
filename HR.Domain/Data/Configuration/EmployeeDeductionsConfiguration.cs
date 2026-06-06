using HR.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Domain.Data.Configuration
{
    public class EmployeeDeductionsConfiguration : IEntityTypeConfiguration<EmployeeDeductions>
    {
        public void Configure(EntityTypeBuilder<EmployeeDeductions> builder)
        {
            builder.Property(e => e.Unit)
                   .HasConversion<string>()
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(e => e.Quantity)
                   .IsRequired()
                   .HasPrecision(18, 2);

            builder.Property(e => e.CalculatedAmount)
                   .IsRequired()
                   .HasPrecision(18, 2);

            builder.Property(e => e.Reason)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.HasOne(ed => ed.Employee)
                   .WithMany(e => e.EmployeeDeductions)
                   .HasForeignKey(ed => ed.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
