using HR.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Domain.Data.Configuration
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.Property(l => l.IsRemote)
                   .IsRequired();

            builder.Property(l => l.Address)
                   .HasMaxLength(500);

            builder.Property(l => l.Lat)
               .HasPrecision(10, 7);

            builder.Property(l => l.Long)
                   .HasPrecision(10, 7);
        }
    }
}
