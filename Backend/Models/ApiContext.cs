using Backend.Models.Configurations;
using Backend.Models.Database;
using Backend.Models.ManufacturerEntity;
using Backend.Models.MedicamentEntity;
using Backend.Models.OrderEntity;
using Backend.Models.PharmacyEntity;
using Backend.Models.ProductBalanceEntity;
using Backend.Models.RegisterEntity;
using Backend.Models.ReportEntity;
using Backend.Models.TransactionEntity;
using Backend.Models.UserEntity;
using Backend.Models.WarehouseEntity;
using Backend.Models.WorkingHoursEntity;
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
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProductBalance> ProductBalances { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<OrderState> OrderStates { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderUser> OrderUsers { get; set; }
        public DbSet<OrderProductBalance> OrderProductBalances { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportType> ReportTypes { get; set; }

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
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductBalanceConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProductBalanceConfiguration());
            modelBuilder.ApplyConfiguration(new OrderUserConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStateConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            modelBuilder.ApplyConfiguration(new ReportTypeConfiguration());

            modelBuilder.Entity<PharmacyWorkingHours>()
                .HasKey(pwh => new { pwh.PharmacyId, pwh.WorkingHoursId });

            modelBuilder.Entity<OrderProductBalance>()
                .HasKey(opb => new { opb.OrderId, opb.ProductBalanceId });

            modelBuilder.Entity<OrderUser>()
                .HasKey(ou => new { ou.OrderId, ou.UserId });
        }
    }
}
