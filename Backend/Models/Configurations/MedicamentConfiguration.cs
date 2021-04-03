using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.Configurations
{
    public class MedicamentConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.Property(m => m.PharmaceuticalFormId).HasConversion<int>();
            builder
                .HasOne(m => m.PharmaceuticalForm)
                .WithMany(pf => pf.Medicaments);

            builder
                .HasMany(m => m.RequiredMedicamentAmounts)
                .WithOne(rm => rm.Medicament);
        }
    }
}
