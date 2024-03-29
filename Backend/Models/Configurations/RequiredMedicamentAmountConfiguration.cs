﻿using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.Configurations
{
    public class RequiredMedicamentAmountConfiguration : IEntityTypeConfiguration<RequiredMedicamentAmount>
    {
        public void Configure(EntityTypeBuilder<RequiredMedicamentAmount> builder)
        {
            builder
                .HasOne(rm => rm.Pharmacy)
                .WithMany(p => p.RequiredMedicamentAmounts);

            builder
                .HasOne(rm => rm.Medicament)
                .WithMany(m => m.RequiredMedicamentAmounts);
            
            builder.HasQueryFilter(rm => !rm.IsSoftDeleted);
        }
    }
}
