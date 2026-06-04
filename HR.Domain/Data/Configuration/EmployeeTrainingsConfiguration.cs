using HR.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Domain.Data.Configuration
{
    public class EmployeeTrainingsConfiguration : IEntityTypeConfiguration<EmployeeTrainings>
    {
        public void Configure(EntityTypeBuilder<EmployeeTrainings> builder)
        {
            builder.HasKey(et => new { et.EmployeeId, et.TrainingId });

            builder.Property(et => et.CompletionStatus)
                   .HasConversion<string>()
                   .HasMaxLength(30)
                   .IsRequired();


            builder.HasOne(et => et.Employee)
                   .WithMany(e => e.EmployeeTrainings)
                   .HasForeignKey(et => et.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(et => et.Training)
                   .WithMany(t => t.EmployeeTrainings)
                   .HasForeignKey(et => et.TrainingId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
