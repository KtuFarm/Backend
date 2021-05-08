using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.Configurations
{
    public class OrderStateConfiguration:IEntityTypeConfiguration<OrderState>
    {
        public void Configure(EntityTypeBuilder<OrderState> builder)
        {
            builder.Property(os => os.Id).HasConversion<int>();
            builder.HasData(
                Enum.GetValues(typeof(OrderStateId))
                    .Cast<OrderStateId>()
                    .Select(os => new OrderState
                    {
                        Id = os,
                        Name = os.ToString()
                    })
            );


            builder
                .HasMany(os => os.Orders)
                .WithOne(o => o.OrderState);
        }
    }
}
