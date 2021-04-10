using Backend.Exceptions;
using Backend.Models.DTO;
using Backend.Services;
using Backend.Services.Interfaces;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace BackendTests
{
    public class MedicamentValidatorTests
    {
        private IMedicamentDTOValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new MedicamentDTOValidator();
        }
        
        [Test]
        public void TestValidDto()
        {
            var dto = new CreateMedicamentDTO
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

            _validator.ValidateCreateMedicamentDto(dto);
            
            Pass();
        }

        [Test]
        public void TestInvalidName()
        {
            var dto = new CreateMedicamentDTO
            {
                Name = "",
                ActiveSubstance = "test",
                BarCode = "123456",
                PharmaceuticalFormId = 1,
                Country = "test",
                IsPrescriptionRequired = false,
                BasePrice = 10,
                Surcharge = 100,
                IsReimbursed = false
            };
            
            Throws<DtoValidationException>(() => _validator.ValidateCreateMedicamentDto(dto));
        }
        
        [Test]
        public void TestInvalidBarcode()
        {
            var dto = new CreateMedicamentDTO
            {
                Name = "test",
                ActiveSubstance = "test",
                BarCode = "abc",
                PharmaceuticalFormId = 1,
                Country = "test",
                IsPrescriptionRequired = false,
                BasePrice = 10,
                Surcharge = 100,
                IsReimbursed = false
            };
            
            Throws<DtoValidationException>(() => _validator.ValidateCreateMedicamentDto(dto));
        }

        [Test]
        public void TestInvalidForm()
        {
            var dto = new CreateMedicamentDTO
            {
                Name = "test",
                ActiveSubstance = "test",
                BarCode = "123456",
                PharmaceuticalFormId = 10,
                Country = "test",
                IsPrescriptionRequired = false,
                BasePrice = 10,
                Surcharge = 100,
                IsReimbursed = false
            };
            
            Throws<DtoValidationException>(() => _validator.ValidateCreateMedicamentDto(dto));
        }

        [Test]
        public void TestInvalidPrice()
        {
            var dto = new CreateMedicamentDTO
            {
                Name = "test",
                ActiveSubstance = "test",
                BarCode = "123456",
                PharmaceuticalFormId = 1,
                Country = "test",
                IsPrescriptionRequired = false,
                BasePrice = -10,
                Surcharge = 100,
                IsReimbursed = false
            };
            
            Throws<DtoValidationException>(() => _validator.ValidateCreateMedicamentDto(dto));
        }
        
        [Test]
        public void TestInvalidReimbursePercentage()
        {
            var dto = new CreateMedicamentDTO
            {
                Name = "test",
                ActiveSubstance = "test",
                BarCode = "123456",
                PharmaceuticalFormId = 1,
                Country = "test",
                IsPrescriptionRequired = false,
                BasePrice = 10,
                Surcharge = 100,
                IsReimbursed = true,
                ReimbursePercentage = 200
            };
            
            Throws<DtoValidationException>(() => _validator.ValidateCreateMedicamentDto(dto));
        }
    }
}
