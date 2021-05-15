using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace Backend.Models.Configurations
{
    public class ReportTypeConfiguration : IEntityTypeConfiguration<ReportType>
    {
        public void Configure(EntityTypeBuilder<ReportType> builder)
        {
            builder.Property(rt => rt.Id).HasConversion<int>();
            builder.HasData(
                Enum.GetValues(typeof(ReportTypeId))
                    .Cast<ReportTypeId>()
                    .Select(rt => new ReportType
                    {
                        Id = rt,
                        Name = rt.ToString()
                    })
            );

            builder
                .HasMany(rt => rt.Reports)
                .WithOne(r => r.ReportType);
        }
    }
}
