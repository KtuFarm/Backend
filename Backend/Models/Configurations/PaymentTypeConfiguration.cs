using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace Backend.Models.Configurations
{
    public class PaymentTypeConfiguration: IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.Property(pt => pt.Id).HasConversion<int>();
            builder.HasData(
                Enum.GetValues(typeof(PaymentTypeId))
                    .Cast<PaymentTypeId>()
                    .Select(pt => new PaymentType()
                    {
                        Id = pt,
                        Name = pt.ToString()
                    })
            );

            builder
                .HasMany(pt => pt.Transactions)
                .WithOne(t => t.PaymentType);
        }
    }
}
