using HR.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Domain.Data.Configuration
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.Property(lt => lt.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(lt => lt.Name).IsUnique();
        }
    }
}
