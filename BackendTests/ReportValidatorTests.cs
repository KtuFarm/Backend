using Backend.Exceptions;
using Backend.Models.ReportEntity.DTO;
using Backend.Services.Validators.ReportDTOValidator;
using NUnit.Framework;
using System;
using static NUnit.Framework.Assert;

namespace BackendTests
{
    class ReportValidatorTests
    {
        private IReportDTOValidator _validator;

        private static CreateReportDTO ValidCreateDto =>
            new()
            {
                DateFrom = new DateTime(2021, 5, 15),
                DateTo = new DateTime(2021, 5, 20),
                ReportTypeId = 1
            };

        [SetUp]
        public void Setup()
        {
            _validator = new ReportDTOValidator();
        }

        [Test]
        public void TestValidDto()
        {
            var dto = ValidCreateDto;

            _validator.ValidateCreateReportDTO(dto);

            Pass();
        }

        [Test]
        public void TestInvalidReportTypeId()
        {
            var dto = ValidCreateDto;
            dto.ReportTypeId = -1;

            Throws<DtoValidationException>(() => _validator.ValidateCreateReportDTO(dto));
        }
    }
}
