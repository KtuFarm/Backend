using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.OrderEntity
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.OrderStateId).HasConversion<int>();

            builder
                .HasOne(o => o.OrderState)
                .WithMany(os => os.Orders);

            builder
                .HasOne(o => o.Warehouse)
                .WithMany(w => w.Orders);

            builder
                .HasMany(o => o.OrderUsers)
                .WithOne(ou => ou.Order);

            builder
                .HasMany(o => o.OrderProductBalances)
                .WithOne(opb => opb.Order);

            builder
                .HasOne(o => o.Pharmacy)
                .WithMany(p => p.Orders);

            builder.HasQueryFilter(o => !o.IsSoftDeleted);
        }
    }
}
