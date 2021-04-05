using System.Collections.Generic;
using System.Linq;
using Backend.Models.Database;

namespace Backend.Models.Seed
{
    public static class PharmacySeed
    {
        public static void EnsureCreated(ApiContext context)
        {
            var testPharmacy = context.Pharmacies.FirstOrDefault(p => p.Id == 1);
            if (testPharmacy != null) return;

            var pharmacy = new Pharmacy
            {
                Id = 1,
                Address = "Studentu g. 69",
                City = "Kaunas",
                Registers = new List<Register>(),
            };
            
            context.Pharmacies.Add(pharmacy);

            var workingHours = context.WorkingHours.Where(wh => wh.Id >= 1 && wh.Id <= 5).ToList();
            foreach (var hours in workingHours)
            {
                context.PharmacyWorkingHours.Add(
                    new PharmacyWorkingHours(pharmacy, hours)
                );
            }
            
            context.SaveChanges();
        }
    }
}
