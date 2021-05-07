using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace Backend.Models.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Id).HasConversion<int>();
            builder.HasData(
                Enum.GetValues(typeof(DepartmentId))
                    .Cast<DepartmentId>()
                    .Select(d => new Department
                    {
                        Id = d,
                        Name = d.ToString()
                    })
            );

            builder
                .HasMany(d => d.Users)
                .WithOne(u => u.Department);
        }
    }
}
