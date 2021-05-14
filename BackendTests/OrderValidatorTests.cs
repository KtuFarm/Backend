using Backend.Exceptions;
using Backend.Models.DTO;
using Backend.Models.OrderEntity.DTO;
using Backend.Services.Validators.OrderDTOValidator;
using NUnit.Framework;
using System.Collections.Generic;
using static NUnit.Framework.Assert;

namespace BackendTests
{
    public class OrderValidatorTests
    {
        private IOrderDTOValidator _validator;

        private static CreateOrderDTO ValidCreateDto =>
            new()
            {
                WarehouseId = 1,
                Products = new List<TransactionProductDTO>
                {
                    new()
                    {
                        ProductBalanceId = 1,
                        Amount = 5.5
                    }
                }
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
    }
}
