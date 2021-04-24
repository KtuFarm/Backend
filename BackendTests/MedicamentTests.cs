using Backend.Models.Database;
using Backend.Models.DTO;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace BackendTests
{
    public class MedicamentTests
    {
        private static CreateMedicamentDTO ValidCreateDto =>
            new()
            {
                Name = "test",
                ActiveSubstance = "test",
                BarCode = "123456",
                PharmaceuticalFormId = 1,
                Country = "test",
                IsPrescriptionRequired = false,
                BasePrice = 10,
                Surcharge = 100,
                IsReimbursed = false
            };

        private static Medicament ControlMedicament =>
            new()
            {
                Name = "test",
                ActiveSubstance = "test",
                BarCode = "123456",
                PharmaceuticalFormId = (PharmaceuticalFormId)1,
                Country = "test",
                IsPrescriptionRequired = false,
                BasePrice = 10,
                Surcharge = 100,
                IsReimbursed = false
            };
        
        [Test]
        public void TestCreateMedicamentFromDto()
        {
            var newMedicament = new Medicament(ValidCreateDto);

            AreEqual(ControlMedicament, newMedicament);
        }

        [Test]
        public void TestEditMedicamentWithDto()
        {
            var medicament = ControlMedicament;
            var editDto = new EditMedicamentDTO()
            {
                BasePrice = 100,
                Surcharge = 50.0
            };
            
            medicament.UpdateFromDTO(editDto);

            bool wasUpdatedSuccessfully =
                medicament.BasePrice == editDto.BasePrice &&
                medicament.Surcharge == editDto.Surcharge;
            
            IsTrue(wasUpdatedSuccessfully);
        }
    }
}
