using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Common;
using Backend.Models.PharmacyEntity;
using Backend.Models.WarehouseEntity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.ProductBalanceEntity
{
    public class ProductBalanceSeed : ISeeder
    {
        private readonly ApiContext _context;

        private const int MedicamentsCount = 3;

        public ProductBalanceSeed(ApiContext context)
        {
            _context = context;
        }

        public async Task EnsureCreated()
        {
            if (!ShouldSeed()) return;

            var pharmacy = _context.Pharmacies.First(p => p.Id == 1);
            var pharmacyBalances = SeedPharmacyProducts(pharmacy);

            var warehouse = _context.Warehouses.First(w => w.Id == 1);
            var warehouseBalances = SeedWarehouseProducts(warehouse);

            await _context.AddRangeAsync(pharmacyBalances);
            await _context.AddRangeAsync(warehouseBalances);
            await _context.SaveChangesAsync();
        }

        private IEnumerable<ProductBalance> SeedPharmacyProducts(Pharmacy pharmacy)
        {
            var medicaments = _context.Medicaments.Where(m => m.Id <= MedicamentsCount).ToList();
            var balances = medicaments.Select((medicament, i) => new ProductBalance
                {
                    Id = i + 1,
                    Amount = 10,
                    ExpirationDate = DateTime.Now.AddYears(1),
                    Medicament = medicament,
                    Pharmacy = pharmacy
                })
                .ToList();
            return balances;
        }

        private IEnumerable<ProductBalance> SeedWarehouseProducts(Warehouse warehouse)
        {
            var medicaments = _context.Medicaments.Where(m => m.Id <= MedicamentsCount).ToList();
            var balances = medicaments.Select((medicament, i) => new ProductBalance
                {
                    Id = i + MedicamentsCount + 1,
                    Amount = 100,
                    ExpirationDate = DateTime.Now.AddYears(1),
                    Medicament = medicament,
                    Warehouse = warehouse
                })
                .ToList();
            return balances;
        }

        private bool ShouldSeed()
        {
            var balance = _context.ProductBalances.IgnoreQueryFilters().FirstOrDefault(m => m.Id == 1);
            return balance == null;
        }
    }
}
