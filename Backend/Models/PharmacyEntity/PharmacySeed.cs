using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Common;
using Backend.Models.Database;
using Backend.Models.RegisterEntity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.PharmacyEntity
{
    public class PharmacySeed : ISeeder
    {
        private readonly ApiContext _context;

        public PharmacySeed(ApiContext context)
        {
            _context = context;
        }

        public async Task EnsureCreated()
        {
            if (!ShouldSeed()) return;

            var pharmacy = new Pharmacy
            {
                Id = 1,
                Address = "Studentu g. 69",
                City = "Kaunas",
                Registers = new List<Register>()
            };

            await _context.Pharmacies.AddAsync(pharmacy);

            var workingHours = _context.WorkingHours.Where(wh => wh.Id >= 1 && wh.Id <= 5).ToList();
            foreach (var hours in workingHours)
            {
                await _context.PharmacyWorkingHours.AddAsync(
                    new PharmacyWorkingHours(pharmacy, hours)
                );
            }

            await _context.SaveChangesAsync();
        }

        private bool ShouldSeed()
        {
            var pharmacy = _context.Pharmacies.IgnoreQueryFilters().FirstOrDefault(p => p.Id == 1);
            return pharmacy == null;
        }
    }
}
