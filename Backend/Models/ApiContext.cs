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
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<RequiredMedicamentAmount> RequiredMedicamentAmounts { get; set; }
        public DbSet<DayOfWeek> DaysOfWeek { get; set; }
        public DbSet<WorkingHours> WorkingHours { get; set; }
        public DbSet<PharmacyWorkingHours> PharmacyWorkingHours { get; set; }
        public DbSet<EmployeeState> EmployeeState { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProductBalance> ProductBalances { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RegisterConfiguration());
            modelBuilder.ApplyConfiguration(new PharmacyConfiguration());
            modelBuilder.ApplyConfiguration(new PharmaceuticalFormConfiguration());
            modelBuilder.ApplyConfiguration(new MedicamentConfiguration());
            modelBuilder.ApplyConfiguration(new ManufacturerConfiguration());
            modelBuilder.ApplyConfiguration(new RequiredMedicamentAmountConfiguration());
            modelBuilder.ApplyConfiguration(new DayOfWeekConfiguration());
            modelBuilder.ApplyConfiguration(new WorkingHoursConfiguration());
            modelBuilder.ApplyConfiguration(new PharmacyWorkingHoursConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeStateConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductBalanceConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());

            modelBuilder.Entity<PharmacyWorkingHours>()
                .HasKey(pwh => new { pwh.PharmacyId, pwh.WorkingHoursId });
        }
    }
}
