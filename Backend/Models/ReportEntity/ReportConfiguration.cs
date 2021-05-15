using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.ReportEntity
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.Property(r => r.ReportTypeId).HasConversion<int>();

            builder
                .HasOne(r => r.User)
                .WithMany(u => u.Reports);

            builder
                .HasOne(r => r.Pharmacy)
                .WithMany(p => p.Reports);

            builder.HasQueryFilter(r => !r.IsSoftDeleted);
        }
    }
}
