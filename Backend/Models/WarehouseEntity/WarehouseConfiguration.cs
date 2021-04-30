using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.WarehouseEntity
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder
                .HasMany(w => w.Employees)
                .WithOne(u => u.Warehouse);

            builder
                .HasMany(w => w.Products)
                .WithOne(pb => pb.Warehouse);

            builder.HasQueryFilter(p => !p.IsSoftDeleted);
        }
    }
}
