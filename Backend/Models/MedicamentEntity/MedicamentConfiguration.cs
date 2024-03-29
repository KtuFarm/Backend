﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.MedicamentEntity
{
    public class MedicamentConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.Property(m => m.PharmaceuticalFormId).HasConversion<int>();
            builder
                .HasOne(m => m.PharmaceuticalForm)
                .WithMany(pf => pf.Medicaments);

            builder
                .HasOne(m => m.Manufacturer)
                .WithMany(m => m.Medicaments);

            builder
                .HasMany(m => m.RequiredMedicamentAmounts)
                .WithOne(rm => rm.Medicament);

            builder
                .HasMany(m => m.Balances)
                .WithOne(pb => pb.Medicament);

            builder.HasQueryFilter(m => !m.IsSoftDeleted);
        }
    }
}
