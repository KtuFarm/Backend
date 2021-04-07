using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.EmployeeStateId).HasConversion<int>();

            builder
                .HasOne(u => u.Pharmacy)
                .WithMany(p => p.Pharmacists);

            builder
                .HasMany(u => u.Manufacturers)
                .WithOne(m => m.Supplier);

            builder
                .HasOne(u => u.EmployeeState)
                .WithMany(ws => ws.Workers);
        }
    }
}
