namespace Backend.Models.Common
{
    public class Seeder
    {
        private readonly ApiContext _context;
        private readonly ISeeder[] _seeders;

        public Seeder(ApiContext context, ISeeder[] seeders)
        {
            _context = context;
            _seeders = seeders;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();

            foreach (var seeder in _seeders)
            {
                seeder.EnsureCreated();
            }
        }
    }
}
