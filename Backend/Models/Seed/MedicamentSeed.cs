using Backend.Models.Database;

namespace Backend.Models.Seed
{
    public class MedicamentSeed
    {
        public static void EnsureCreated(ApiContext context)
        {
            context.Medicaments.AddRange(
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
                    PharmaceuticalFormId = PharmaceuticalFormId.Spray
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
                    PharmaceuticalFormId = PharmaceuticalFormId.Tablets
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
                    PharmaceuticalFormId = PharmaceuticalFormId.Tablets
                }, new Medicament
                {
                    Id = 4,
                    Name = "TestDrug004",
                    ActiveSubstance = "<Redacted>",
                    BarCode = "00004",
                    IsPrescriptionRequired = false,
                    IsReimbursed = false,
                    Country = "France",
                    BasePrice = 69.00M,
                    Surcharge = 20,
                    PharmaceuticalFormId = PharmaceuticalFormId.Ointment
                }
            );
        }
    }
}
