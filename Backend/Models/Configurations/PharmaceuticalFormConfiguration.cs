using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace Backend.Models.Configurations
{
    public class PharmaceuticalFormConfiguration : IEntityTypeConfiguration<PharmaceuticalForm>
    {
        public void Configure(EntityTypeBuilder<PharmaceuticalForm> builder)
        {
            builder.Property(pf => pf.Id).HasConversion<int>();
            builder.HasData(
                Enum.GetValues(typeof(PharmaceuticalFormId))
                    .Cast<PharmaceuticalFormId>()
                    .Select(pf => new PharmaceuticalForm
                    {
                        Id = pf,
                        Name = pf.ToString()
                    })
            );

            builder
                .HasMany(pf => pf.Medicaments)
                .WithOne(m => m.PharmaceuticalForm);
        }
    }
}
