using Backend.Models.Configurations;
using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class ApiContext : DbContext
    {
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<PharmaceuticalForm> PharmaceuticalForms { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<RequiredMedicamentAmount> RequiredMedicamentAmounts { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RegisterConfiguration());
            modelBuilder.ApplyConfiguration(new PharmacyConfiguration());
            modelBuilder.ApplyConfiguration(new PharmaceuticalFormConfiguration());
            modelBuilder.ApplyConfiguration(new MedicamentConfiguration());
            modelBuilder.ApplyConfiguration(new RequiredMedicamentAmountConfiguration());
        }
    }
}
