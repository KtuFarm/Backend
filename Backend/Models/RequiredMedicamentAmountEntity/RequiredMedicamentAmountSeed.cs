using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Common;
using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.RequiredMedicamentAmountEntity
{
    public class RequiredMedicamentAmountSeed : ISeeder
    {
        private readonly ApiContext _context;

        private const int RequiredAmount = 10;

        public RequiredMedicamentAmountSeed(ApiContext context)
        {
            _context = context;
        }

        public async Task EnsureCreated()
        {
            if (!ShouldSeed()) return;

            var pharmacy = _context.Pharmacies.First(p => p.Id == 1);
            var medicament = _context.Medicaments.First(w => w.Id == 1);

            _context.Add(new RequiredMedicamentAmount
            {
                Id = 1,
                Amount = RequiredAmount,
                Medicament = medicament,
                Pharmacy = pharmacy
            });

            await _context.SaveChangesAsync();
        }

        private bool ShouldSeed()
        {
            var balance = _context.RequiredMedicamentAmounts.IgnoreQueryFilters().FirstOrDefault(m => m.Id == 1);
            return balance == null;
        }
    }
}
