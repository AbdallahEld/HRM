using HR.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Domain.Data.Configuration
{
    public class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
    {
        public void Configure(EntityTypeBuilder<Payroll> builder)
        {
            builder.Property(p => p.Salary)
                   .IsRequired()
                   .HasPrecision(18, 2);

            builder.Property(p => p.NetPay)
                   .IsRequired()
                   .HasPrecision(18, 2);

            builder.HasOne(p => p.Employee)
                   .WithMany(e => e.Payrolls)
                   .HasForeignKey(p => p.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
