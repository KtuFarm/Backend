using System.Collections.Generic;
using Backend.Exceptions;
using Backend.Models.PharmacyEntity.DTO;
using Backend.Models.WorkingHoursEntity.DTO;
using Backend.Services.Validators.PharmacyDTOValidator;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace BackendTests
{
    public class PharmacyValidatorTests
    {
        private IPharmacyDTOValidator _validator;

        private static CreatePharmacyDTO ValidCreateDto =>
            new()
            {
                Address = "Test Street",
                City = "Test",
                RegistersCount = 1,
                WorkingHours = new List<WorkingHoursDTO>
                {
                    new()
                    {
                        OpenTime = "09:00",
                        CloseTime = "18:00",
                        DayOfWeek = 1
                    }
                }
            };

        [SetUp]
        public void SetUp()
        {
            _validator = new PharmacyDTOValidator();
        }

        [Test]
        public void TestValidDto()
        {
            var dto = ValidCreateDto;

            _validator.ValidateCreatePharmacyDTO(dto);

            Pass();
        }

        [Test]
        public void TestInvalidAddress()
        {
            var dto = ValidCreateDto;
            dto.Address = "";

            Throws<DtoValidationException>(() => _validator.ValidateCreatePharmacyDTO(dto));
        }

        [Test]
        public void TestInvalidCity()
        {
            var dto = ValidCreateDto;
            dto.City = "";

            Throws<DtoValidationException>(() => _validator.ValidateCreatePharmacyDTO(dto));
        }

        [Test]
        public void TestInvalidRegistersCount()
        {
            var dto = ValidCreateDto;
            dto.RegistersCount = -1;

            Throws<DtoValidationException>(() => _validator.ValidateCreatePharmacyDTO(dto));
        }


        [Test]
        public void TestInvalidWorkingHours()
        {
            var dto = ValidCreateDto;
            dto.WorkingHours.Clear();

            Throws<DtoValidationException>(() => _validator.ValidateCreatePharmacyDTO(dto));
        }

        [Test]
        public void TestValidEditPharmacyDto()
        {
            var dto = new EditPharmacyDTO
            {
                Address = "Test 2",
                City = "Kaunas",
                WorkingHours = new List<WorkingHoursDTO>
                {
                    new()
                    {
                        OpenTime = "09:00",
                        CloseTime = "17:00",
                        DayOfWeek = 5
                    }
                }
            };

            _validator.ValidateEditPharmacyDTO(dto);

            Pass();
        }

        [Test]
        public void TestInvalidEditAddress()
        {
            var dto = new EditPharmacyDTO
            {
                Address = ""
            };

            Throws<DtoValidationException>(() => _validator.ValidateEditPharmacyDTO(dto));
        }

        [Test]
        public void TestInvalidEditCity()
        {
            var dto = new EditPharmacyDTO
            {
                City = ""
            };

            Throws<DtoValidationException>(() => _validator.ValidateEditPharmacyDTO(dto));
        }

        [Test]
        public void TestInvalidEditWorkingHours()
        {
            var dto = new EditPharmacyDTO
            {
                WorkingHours = new List<WorkingHoursDTO>()
            };

            Throws<DtoValidationException>(() => _validator.ValidateEditPharmacyDTO(dto));
        }
    }
}
