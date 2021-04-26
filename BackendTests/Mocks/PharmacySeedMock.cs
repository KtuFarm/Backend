using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Backend.Models.Database;
using Backend.Models.PharmacyEntity;
using Backend.Models.RegisterEntity;

namespace BackendTests.Mocks
{
    public static class PharmacySeedMock
    {
        public static int PharmacyWorkingHoursCount => 5;

        public static void EnsureCreated(ApiContext context)
        {
            var testPharmacy = context.Pharmacies.FirstOrDefault(p => p.Id == 1);
            if (testPharmacy != null) return;

            var pharmacies = new List<Pharmacy>
            {
                new()
                {
                    Id = 1,
                    Address = "Test str.",
                    City = "City",
                    Registers = new List<Register>()
                },
                new()
                {
                    Id = 2,
                    Address = "Test str.",
                    City = "Deleted",
                    Registers = new List<Register>(),
                    IsSoftDeleted = true
                }
            };
            SetWorkingHours(context, pharmacies);

            context.Pharmacies.AddRange(pharmacies);
            context.SaveChanges();
        }

        private static void SetWorkingHours(ApiContext context, IEnumerable<Pharmacy> pharmacies)
        {
            var workingHours = context.WorkingHours.Where(wh => wh.Id >= 1 && wh.Id <= 5).ToList();
            foreach (var pharmacy in pharmacies)
            foreach (var hours in workingHours)
                context.PharmacyWorkingHours.Add(
                    new PharmacyWorkingHours(pharmacy, hours)
                );
        }
    }
}
