using Backend.Models.DTO;
using Backend.Models.OrderEntity.DTO;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Backend.Services.Validators.OrderDTOValidator
{
    public class OrderDTOValidator : DTOValidator, IOrderDTOValidator
    {
        public void ValidateCreateOrderDto(CreateOrderDTO dto)
        {
            ValidateNumberIsPositive(dto.WarehouseId, "warehouseId");
            ValidateProducts(dto.Products);
        }

        [AssertionMethod]
        private static void ValidateProducts(IEnumerable<TransactionProductDTO> products)
        {
            foreach (var product in products)
            {
                ValidateNumberIsPositive(product.Amount, "amount");
                ValidateNumberIsPositive(product.ProductBalanceId, "productBalanceId");
            }
        }
    }
}
