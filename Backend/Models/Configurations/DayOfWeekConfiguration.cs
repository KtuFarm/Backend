using System;
using System.Linq;
using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DayOfWeek = Backend.Models.Database.DayOfWeek;

namespace Backend.Models.Configurations
{
    public class DayOfWeekConfiguration : IEntityTypeConfiguration<DayOfWeek>
    {
        public void Configure(EntityTypeBuilder<DayOfWeek> builder)
        {
            builder.Property(dw => dw.Id).HasConversion<int>();
            builder.HasData(
                Enum.GetValues(typeof(DayOfWeekId))
                    .Cast<DayOfWeekId>()
                    .Select(dw => new DayOfWeek
                    {
                        Id = dw,
                        Name = dw.ToString()
                    })
            );

            builder
                .HasMany(dw => dw.WorkingHours)
                .WithOne(wh => wh.DayOfWeek);
        }
    }
}
