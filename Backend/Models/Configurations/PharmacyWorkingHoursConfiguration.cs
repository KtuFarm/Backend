using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.Configurations
{
    public class PharmacyWorkingHoursConfiguration: IEntityTypeConfiguration<PharmacyWorkingHours>
    {
        public void Configure(EntityTypeBuilder<PharmacyWorkingHours> builder)
        {
            builder
                .HasOne(pwh => pwh.Pharmacy)
                .WithMany(p => p.PharmacyWorkingHours);

            builder
                .HasOne(pwh => pwh.WorkingHours)
                .WithMany(wh => wh.PharmacyWorkingHours);
        }
    }
}
