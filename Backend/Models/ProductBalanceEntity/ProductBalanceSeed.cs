using System;
using System.Linq;
using Backend.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.ProductBalanceEntity
{
    public class ProductBalanceSeed : ISeeder
    {
        private readonly ApiContext _context;

        public ProductBalanceSeed(ApiContext context)
        {
            _context = context;
        }

        public void EnsureCreated()
        {
            if (!ShouldSeed()) return;

            var pharmacy = _context.Pharmacies.First(p => p.Id == 1);
            var medicaments = _context.Medicaments.Where(m => m.Id <= 3).ToList();
            var balances = medicaments.Select((medicament, i) => new ProductBalance
                {
                    Id = i + 1,
                    Amount = 10,
                    ExpirationDate = DateTime.Now.AddYears(1),
                    Medicament = medicament,
                    Pharmacy = pharmacy
                })
                .ToList();

            _context.AddRange(balances);
            _context.SaveChanges();
        }

        private bool ShouldSeed()
        {
            var balance = _context.ProductBalances.IgnoreQueryFilters().FirstOrDefault(m => m.Id == 1);
            return balance == null;
        }
    }
}
