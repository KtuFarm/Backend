using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.WarehouseEntity
{
    public class WarehouseSeed : ISeeder
    {
        private readonly ApiContext _context;

        public WarehouseSeed(ApiContext context)
        {
            _context = context;
        }

        public async Task EnsureCreated()
        {
            if (!ShouldSeed()) return;

            for (int i = 1; i <= 3; i++)
            {
                var warehouse = new Warehouse
                {
                    Id = i,
                    Address = $"Warehouse Str. {i * 10}",
                    City = "Kaunas",
                };

                _context.Add(warehouse);
            }

            await _context.SaveChangesAsync();
        }

        private bool ShouldSeed()
        {
            var warehouse = _context.Warehouses.IgnoreQueryFilters().FirstOrDefault(p => p.Id == 1);
            return warehouse == null;
        }
    }
}
