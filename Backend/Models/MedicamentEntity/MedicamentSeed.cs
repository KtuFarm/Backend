using System.Linq;
using Backend.Models.Common;
using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.MedicamentEntity
{
    public class MedicamentSeed : ISeeder
    {
        private readonly ApiContext _context;

        public MedicamentSeed(ApiContext context)
        {
            _context = context;
        }

        public void EnsureCreated()
        {
            if (!ShouldSeed()) return;

            _context.Medicaments.AddRange(
                new Medicament
                {
                    Id = 1,
                    Name = "TestDrug001",
                    ActiveSubstance = "Dopamine",
                    BarCode = "00001",
                    IsPrescriptionRequired = false,
                    IsReimbursed = false,
                    Country = "Lithuania",
                    BasePrice = 69.00M,
                    Surcharge = 20,
                    PharmaceuticalFormId = PharmaceuticalFormId.Spray,
                    Manufacturer = _context.Manufacturers.FirstOrDefault(m => m.Id == 2)
                },
                new Medicament
                {
                    Id = 2,
                    Name = "TestDrug002",
                    ActiveSubstance = "Methamphetamine",
                    BarCode = "00002",
                    IsPrescriptionRequired = true,
                    IsReimbursed = false,
                    Country = "Germany",
                    BasePrice = 69.00M,
                    Surcharge = 20,
                    PharmaceuticalFormId = PharmaceuticalFormId.Tablets,
                    Manufacturer = _context.Manufacturers.FirstOrDefault(m => m.Id == 1)
                },
                new Medicament
                {
                    Id = 3,
                    Name = "TestDrug003",
                    ActiveSubstance = "Adderall",
                    BarCode = "00003",
                    IsPrescriptionRequired = true,
                    IsReimbursed = true,
                    Country = "USA",
                    BasePrice = 68.99M,
                    Surcharge = 20,
                    PharmaceuticalFormId = PharmaceuticalFormId.Tablets,
                    Manufacturer = _context.Manufacturers.FirstOrDefault(m => m.Id == 3)
                }, new Medicament
                {
                    Id = 4,
                    Name = "TestDrug004",
                    ActiveSubstance = "<Redacted>",
                    BarCode = "00004",
                    IsPrescriptionRequired = false,
                    IsReimbursed = false,
                    Country = "Germany",
                    BasePrice = 69.00M,
                    Surcharge = 20,
                    PharmaceuticalFormId = PharmaceuticalFormId.Ointment,
                    Manufacturer = _context.Manufacturers.FirstOrDefault(m => m.Id == 1)
                }
            );

            _context.SaveChanges();
        }

        private bool ShouldSeed()
        {
            var medicament = _context.Medicaments.IgnoreQueryFilters().FirstOrDefault(m => m.Id == 1);
            return medicament == null;
        }
    }
}
