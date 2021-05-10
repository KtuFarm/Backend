using Backend.Models.OrderEntity.DTO;
using Backend.Services.Validators.OrderDTOValidator;
using System;
using System.Collections.Generic;
using Backend.Models.DTO;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using Backend.Exceptions;

namespace BackendTests
{
    class OrderValidatorTests
    {
        private IOrderDTOValidator _validator;

        private static CreateOrderDTO ValidCreateDto =>
            new()
            {
                WarehouseId = 1,
                PharmacyId = 1,
                Products = new List<TransactionProductDTO>
                {
                    new()
                    {
                        ProductBalanceId = 1,
                        Amount = 5.5
                    }
                },
                CreationDate = new DateTime(2021, 5, 10),
                DeliveryDate = new DateTime(2021, 5, 11)
            };

        [SetUp]
        public void Setup()
        {
            _validator = new OrderDTOValidator();
        }

        [Test]
        public void TestValidDto()
        {
            var dto = ValidCreateDto;

            _validator.ValidateCreateOrderDto(dto);

            Pass();
        }

        [Test]
        public void TestInvalidWarehouseId()
        {
            var dto = ValidCreateDto;
            dto.WarehouseId = -1;

            Throws<DtoValidationException>(() => _validator.ValidateCreateOrderDto(dto));
        }

        [Test]
        public void TestInvalidPharmacyId()
        {
            var dto = ValidCreateDto;
            dto.PharmacyId = -1;

            Throws<DtoValidationException>(() => _validator.ValidateCreateOrderDto(dto));
        }

        [Test]
        public void TestInvalidProductAmount()
        {
            var dto = ValidCreateDto;
            dto.Products[0].Amount = -1.0;

            Throws<DtoValidationException>(() => _validator.ValidateCreateOrderDto(dto));
        }

        [Test]
        public void TestInvalidProductBalanceId()
        {
            var dto = ValidCreateDto;
            dto.Products[0].ProductBalanceId = -1;

            Throws<DtoValidationException>(() => _validator.ValidateCreateOrderDto(dto));
        }

        [Test]
        public void TestInvalidDateSpan()
        {
            var dto = ValidCreateDto;
            var creationDate = dto.CreationDate;
            dto.CreationDate = new DateTime(creationDate.Year + 1, creationDate.Month, creationDate.Day);

            Throws<DtoValidationException>(() => _validator.ValidateCreateOrderDto(dto));
        }
    }
}
