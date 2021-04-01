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
                .HasOne(p => p.Pharmacy)
                .WithMany(r => r.RequiredMedicamentAmounts);

            builder
                .HasOne(m => m.Medicament)
                .WithMany(r => r.RequiredMedicamentAmounts);
        }
    }
}
