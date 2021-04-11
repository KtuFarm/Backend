using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using Backend.Models.Database;

namespace Backend.Models.Configurations
{
    public class EmployeeStateConfiguration : IEntityTypeConfiguration<EmployeeState>
    {
        public void Configure(EntityTypeBuilder<EmployeeState> builder)
        {
            builder.Property(ws => ws.Id).HasConversion<int>();
            builder.HasData(
                Enum.GetValues(typeof(EmployeeStateId))
                    .Cast<EmployeeStateId>()
                    .Select(ws => new EmployeeState
                    {
                        Id = ws,
                        Name = ws.ToString()
                    })
            );

            builder
                .HasMany(ws => ws.Employees)
                .WithOne(u => u.EmployeeState);
        }
    }
}
