using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Database;
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
        }
    }
}
