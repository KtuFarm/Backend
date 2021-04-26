using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.ProductBalanceEntity
{
    public class ProductBalanceConfiguration: IEntityTypeConfiguration<ProductBalance>
    {
        public void Configure(EntityTypeBuilder<ProductBalance> builder)
        {
            builder
                .HasOne(pb => pb.Medicament)
                .WithMany(m => m.Balances);

            builder
                .HasOne(pb => pb.Pharmacy)
                .WithMany(p => p.Products);

            builder
                .HasOne(pb => pb.Transaction)
                .WithMany(t => t.Medicaments);

            builder.HasQueryFilter(pb => !pb.IsSoftDeleted);
        }
    }
}
