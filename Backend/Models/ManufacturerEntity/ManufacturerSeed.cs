using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.ManufacturerEntity
{
    public class ManufacturerSeed : ISeeder
    {
        private readonly ApiContext _context;

        public ManufacturerSeed(ApiContext context)
        {
            _context = context;
        }

        public async Task EnsureCreated()
        {
            if (!ShouldSeed()) return;

            await _context.Manufacturers.AddRangeAsync(
                new Manufacturer
                {
                    Id = 1,
                    Country = "Germany",
                    Name = "Bayer GmbH"
                },
                new Manufacturer
                {
                    Id = 2,
                    Country = "Lithuania",
                    Name = "LitPharma"
                },
                new Manufacturer
                {
                    Id = 3,
                    Country = "USA",
                    Name = "Pfizer Inc."
                }
            );

            await _context.SaveChangesAsync();
        }

        private bool ShouldSeed()
        {
            var manufacturer = _context.Manufacturers.IgnoreQueryFilters().FirstOrDefault(m => m.Id == 1);
            return manufacturer == null;
        }
    }
}
