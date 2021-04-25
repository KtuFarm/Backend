using Backend.Exceptions;
using Backend.Models.MedicamentEntity.DTO;
using Backend.Services.Validators.MedicamentDTOValidator;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace BackendTests
{
    public class MedicamentValidatorTests
    {
        private IMedicamentDTOValidator _validator;

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

        [SetUp]
        public void SetUp()
        {
            _validator = new MedicamentDTOValidator();
        }

        [Test]
        public void TestValidDto()
        {
            var dto = ValidCreateDto;

            _validator.ValidateCreateMedicamentDto(dto);

            Pass();
        }

        [Test]
        public void TestInvalidName()
        {
            var dto = ValidCreateDto;
            dto.Name = "";

            Throws<DtoValidationException>(() => _validator.ValidateCreateMedicamentDto(dto));
        }

        [Test]
        public void TestInvalidBarcode()
        {
            var dto = ValidCreateDto;
            dto.BarCode = "abc";

            Throws<DtoValidationException>(() => _validator.ValidateCreateMedicamentDto(dto));
        }

        [Test]
        public void TestInvalidForm()
        {
            var dto = ValidCreateDto;
            dto.PharmaceuticalFormId = -1;

            Throws<DtoValidationException>(() => _validator.ValidateCreateMedicamentDto(dto));
        }

        [Test]
        public void TestInvalidPrice()
        {
            var dto = ValidCreateDto;
            dto.BasePrice = -10;

            Throws<DtoValidationException>(() => _validator.ValidateCreateMedicamentDto(dto));
        }

        [Test]
        public void TestInvalidReimbursePercentage()
        {
            var dto = ValidCreateDto;
            dto.ReimbursePercentage = 200;

            Throws<DtoValidationException>(() => _validator.ValidateCreateMedicamentDto(dto));
        }

        [Test]
        public void TestInvalidSurcharge()
        {
            var dto = ValidCreateDto;
            dto.Surcharge = -10;

            Throws<DtoValidationException>(() => _validator.ValidateCreateMedicamentDto(dto));
        }

        [Test]
        public void TestValidEditMedicamentDto()
        {
            var dto = new EditMedicamentDTO
            {
                IsPrescriptionRequired = true,
                BasePrice = 20.00M,
                Surcharge = 50,
                IsReimbursed = false
            };

            _validator.ValidateEditMedicamentDto(dto);

            Pass();
        }

        [Test]
        public void TestInvalidEditSurcharge()
        {
            var dto = new EditMedicamentDTO
            {
                Surcharge = -100
            };

            Throws<DtoValidationException>(() => _validator.ValidateEditMedicamentDto(dto));
        }

        [Test]
        public void TestInvalidEditBasePrice()
        {
            var dto = new EditMedicamentDTO
            {
                BasePrice = -100
            };

            Throws<DtoValidationException>(() => _validator.ValidateEditMedicamentDto(dto));
        }

        [Test]
        public void TestInvalidEditReimbursePercentage()
        {
            var dto = new EditMedicamentDTO
            {
                ReimbursePercentage = -100
            };

            Throws<DtoValidationException>(() => _validator.ValidateEditMedicamentDto(dto));
        }
    }
}
