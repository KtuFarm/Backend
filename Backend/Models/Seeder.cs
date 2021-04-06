using Backend.Models.Seed;

namespace Backend.Models
{
    public class Seeder
    {
        private readonly ApiContext _context;

        public Seeder(ApiContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();

            ManufacturerSeed.EnsureCreated(_context);
            MedicamentSeed.EnsureCreated(_context);
            WorkingHoursSeed.EnsureCreated(_context);
            PharmacySeed.EnsureCreated(_context);

            _context.SaveChanges();
        }
    }
}
