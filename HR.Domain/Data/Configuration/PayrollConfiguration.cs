using HR.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Domain.Data.Configuration
{
    public class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
    {
        public void Configure(EntityTypeBuilder<Payroll> builder)
        {

            builder.Property(p => p.GrossPay)
                   .IsRequired()
                   .HasPrecision(18, 2);

            builder.Property(p => p.TotalDeductions)
                   .IsRequired()
                   .HasPrecision(18, 2);

            builder.Property(p => p.NetPay)
                   .IsRequired()
                   .HasPrecision(18, 2);

            builder.Property(p => p.PayPeriodStart)
                   .IsRequired();

            builder.Property(p => p.PayPeriodEnd)
                   .IsRequired();

            builder.Property(p => p.Currency)
                   .IsRequired()
                   .HasMaxLength(3);

            builder.Property(p => p.PaymentStatus)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.HasIndex(p => new { p.EmployeeId, p.PayPeriodStart, p.PayPeriodEnd })
                   .IsUnique();

            builder.HasOne(p => p.Employee)
                   .WithMany(e => e.Payrolls)
                   .HasForeignKey(p => p.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
