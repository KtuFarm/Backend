using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.Configurations
{
    public class WorkingHoursConfiguration : IEntityTypeConfiguration<WorkingHours>
    {
        public void Configure(EntityTypeBuilder<WorkingHours> builder)
        {
            builder.Property(wh => wh.DayOfWeekId).HasConversion<int>();

            builder
                .HasOne(wh => wh.DayOfWeek)
                .WithMany(dw => dw.WorkingHours);

            builder
                .HasMany(wh => wh.PharmacyWorkingHours)
                .WithOne(pwh => pwh.WorkingHours);
        }
    }
}
