using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.TransactionEntity
{
    public class TransactionConfiguration: IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder
                .HasOne(t => t.Register)
                .WithMany(r => r.Transactions);

            builder
                .HasOne(t => t.Pharmacy)
                .WithMany(p => p.Transactions);

            builder
                .HasOne(t => t.Pharmacist)
                .WithMany(u => u.Transactions);

            builder
                .HasMany(t => t.Medicaments)
                .WithOne(m => m.Transaction);

            builder.Property(t => t.PaymentTypeId).HasConversion<int>();
            builder
                .HasOne(t => t.PaymentType)
                .WithMany(pt => pt.Transactions);

            builder.HasQueryFilter(t => !t.IsSoftDeleted);
        }
    }
}
