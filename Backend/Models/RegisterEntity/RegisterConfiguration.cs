using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.RegisterEntity
{
    public class RegisterConfiguration : IEntityTypeConfiguration<Register>
    {
        public void Configure(EntityTypeBuilder<Register> builder)
        {
            builder
                .HasOne(r => r.Pharmacy)
                .WithMany(p => p.Registers);

            builder
                .HasMany(r => r.Transactions)
                .WithOne(t => t.Register);
            
            builder.HasQueryFilter(r => !r.IsSoftDeleted);
        }
    }
}
