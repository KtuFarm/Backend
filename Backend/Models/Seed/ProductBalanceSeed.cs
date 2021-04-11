using System;
using System.Linq;
using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Seed
{
    public static class ProductBalanceSeed
    {
        public static void EnsureCreated(ApiContext context)
        {
            var testBalance = context.ProductBalances.IgnoreQueryFilters().FirstOrDefault(pb => pb.Id == 1);
            if (testBalance != null) return;

            var pharmacy = context.Pharmacies.First(p => p.Id == 1);
            var medicaments = context.Medicaments.Where(m => m.Id <= 3).ToList();
            var balances = medicaments.Select((medicament, i) => new ProductBalance
                {
                    Id = i + 1,
                    Amount = 10,
                    ExpirationDate = DateTime.Now.AddYears(1),
                    Medicament = medicament,
                    Pharmacy = pharmacy
                })
                .ToList();

            context.AddRange(balances);
            context.SaveChanges();
        }
    }
}
