using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.Configurations
{
    public class PharmacyConfiguration : IEntityTypeConfiguration<Pharmacy>
    {
        public void Configure(EntityTypeBuilder<Pharmacy> builder)
        {
            builder
                .HasMany(p => p.Registers)
                .WithOne(r => r.Pharmacy);

            builder
                .HasMany(p => p.RequiredMedicamentAmounts)
                .WithOne(rm => rm.Pharmacy);

            builder
                .HasMany(p => p.PharmacyWorkingHours)
                .WithOne(pwh => pwh.Pharmacy);

            builder
                .HasMany(p => p.Pharmacists)
                .WithOne(u => u.Pharmacy);

            builder
                .HasMany(p => p.Medicaments)
                .WithOne(pb => pb.Pharmacy);

            builder
                .HasMany(p => p.Transactions)
                .WithOne(t => t.Pharmacy);

            builder.HasQueryFilter(p => !p.IsSoftDeleted);
        }
    }
}
