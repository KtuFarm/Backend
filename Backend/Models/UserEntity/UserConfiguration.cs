using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.UserEntity
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.EmployeeStateId).HasConversion<int>();
            builder.Property(u => u.DepartmentId).HasConversion<int>();

            builder
                .HasOne(u => u.Pharmacy)
                .WithMany(p => p.Pharmacists);

            builder
                .HasMany(u => u.Manufacturers)
                .WithOne(m => m.Supplier);

            builder
                .HasOne(u => u.EmployeeState)
                .WithMany(es => es.Employees);

            builder
                .HasMany(u => u.Transactions)
                .WithOne(t => t.Pharmacist);

            builder
                .HasOne(u => u.Warehouse)
                .WithMany(w => w.Employees);

            builder
                .HasOne(u => u.Department)
                .WithMany(d => d.Users);

            builder
                .HasMany(u => u.OrderUsers)
                .WithOne(ou => ou.User);

            builder.HasQueryFilter(u => !u.IsSoftDeleted);
        }
    }
}
