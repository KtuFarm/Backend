using System.Threading.Tasks;

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

        public async Task Seed()
        {
            await _context.Database.EnsureCreatedAsync();

            foreach (var seeder in _seeders)
            {
                await seeder.EnsureCreated();
            }
        }
    }
}
