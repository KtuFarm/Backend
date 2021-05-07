using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Database;
using Backend.Models.OrderEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.ObjectPool;

namespace Backend.Models.Configurations
{
    public class OrderProductBalanceConfiguration : IEntityTypeConfiguration<OrderProductBalance>
    {
        public void Configure(EntityTypeBuilder<OrderProductBalance> builder)
        {
            builder
                .HasOne(opb => opb.Order)
                .WithMany(o => o.OrderProductBalances);

            builder
                .HasOne(opb => opb.ProductBalance)
                .WithMany(pb => pb.OrderProductBalances);

            builder.HasQueryFilter(opb => !opb.IsSoftDeleted);
        }
    }
}
