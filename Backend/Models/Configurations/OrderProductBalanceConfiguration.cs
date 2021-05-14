using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
