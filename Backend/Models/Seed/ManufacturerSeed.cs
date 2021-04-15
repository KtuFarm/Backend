using Backend.Models.Database;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Seed
{
    public class ManufacturerSeed : ISeeder
    {
        private readonly ApiContext _context;

        public ManufacturerSeed(ApiContext context)
        {
            _context = context;
        }

        public void EnsureCreated()
        {
            if (!ShouldSeed()) return;

            _context.Manufacturers.AddRange(
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

            _context.SaveChanges();
        }

        private bool ShouldSeed()
        {
            var manufacturer = _context.Manufacturers.IgnoreQueryFilters().FirstOrDefault(m => m.Id == 1);
            return manufacturer == null;
        }
    }
}
