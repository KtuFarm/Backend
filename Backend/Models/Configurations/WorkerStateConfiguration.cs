using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using Backend.Models.Database;

namespace Backend.Models.Configurations
{
    public class WorkerStateConfiguration : IEntityTypeConfiguration<WorkerState>
    {
        public void Configure(EntityTypeBuilder<WorkerState> builder)
        {
            builder.Property(ws => ws.Id).HasConversion<int>();
            builder.HasData(
                Enum.GetValues(typeof(WorkerStateId))
                    .Cast<WorkerStateId>()
                    .Select(ws => new WorkerState
                    {
                        Id = ws,
                        Name = ws.ToString()
                    })
            );

            builder
                .HasMany(ws => ws.Workers)
                .WithOne(u => u.WorkerState);
        }
    }
}
