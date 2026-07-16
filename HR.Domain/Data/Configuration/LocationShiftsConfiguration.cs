using HR.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Domain.Data.Configuration
{
    public class LocationShiftsConfiguration : IEntityTypeConfiguration<LocationShifts>
    {
        public void Configure(EntityTypeBuilder<LocationShifts> builder)
        {
            builder.HasKey(ls => new { ls.LocationId, ls.ShiftId });

            builder.HasOne(et => et.Shift)
                   .WithMany(e => e.LocationShifts)
                   .HasForeignKey(et => et.ShiftId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(et => et.Location)
                   .WithMany(t => t.LocationShifts)
                   .HasForeignKey(et => et.LocationId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
