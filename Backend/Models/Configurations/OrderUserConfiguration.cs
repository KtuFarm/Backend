using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.Configurations
{
    public class OrderUserConfiguration : IEntityTypeConfiguration<OrderUser>
    {
        public void Configure(EntityTypeBuilder<OrderUser> builder)
        {
            builder
                .HasOne(ou => ou.Order)
                .WithMany(o => o.OrderUsers);

            builder
                .HasOne(or => or.User)
                .WithMany(u => u.OrderUsers);

            builder.HasQueryFilter(ou => !ou.IsSoftDeleted);
        }
    }
}
