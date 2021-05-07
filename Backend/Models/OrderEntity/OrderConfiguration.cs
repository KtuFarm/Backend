using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                .HasOne(o => o.Pharmacist)
                .WithMany(u => u.Orders);

            builder
                .HasOne(o => o.Courier)
                .WithMany(u => u.Orders);

            builder
                .HasMany(o => o.OrderProductBalances)
                .WithOne(opb => opb.Order);

            builder.HasQueryFilter(o => !o.IsSoftDeleted);
        }
    }
}
