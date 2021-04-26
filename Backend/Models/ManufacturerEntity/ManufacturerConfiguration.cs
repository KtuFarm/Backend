using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.ManufacturerEntity
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder
                .HasMany(m => m.Medicaments)
                .WithOne(m => m.Manufacturer);

            builder
                .HasOne(m => m.Supplier)
                .WithMany(s => s.Manufacturers);
        }
    }
}
