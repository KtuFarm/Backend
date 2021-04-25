using System.Collections.Generic;
using System.Linq;
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

        public void EnsureCreated()
        {
            if (!ShouldSeed()) return;

            var pharmacy = new Pharmacy
            {
                Id = 1,
                Address = "Studentu g. 69",
                City = "Kaunas",
                Registers = new List<Register>()
            };

            _context.Pharmacies.Add(pharmacy);

            var workingHours = _context.WorkingHours.Where(wh => wh.Id >= 1 && wh.Id <= 5).ToList();
            foreach (var hours in workingHours)
            {
                _context.PharmacyWorkingHours.Add(
                    new PharmacyWorkingHours(pharmacy, hours)
                );
            }

            _context.SaveChanges();
        }

        private bool ShouldSeed()
        {
            var pharmacy = _context.Pharmacies.IgnoreQueryFilters().FirstOrDefault(p => p.Id == 1);
            return pharmacy == null;
        }
    }
}
