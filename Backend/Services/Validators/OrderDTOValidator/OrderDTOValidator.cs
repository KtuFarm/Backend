using Backend.Exceptions;
using Backend.Models.Common;
using Backend.Models.DTO;
using Backend.Models.OrderEntity.DTO;
using System;
using System.Collections.Generic;

namespace Backend.Services.Validators.OrderDTOValidator
{
    public class OrderDTOValidator : DTOValidator, IOrderDTOValidator
    {
        public void ValidateCreateOrderDto(CreateOrderDTO dto)
        {
            ValidateNumberIsPositive(dto.PharmacyId, "pharmacyId");
            ValidateNumberIsPositive(dto.WarehouseId, "warehouseId");
            ValidateDateSpan(dto.CreationDate, dto.DeliveryDate);
            ValidateProducts(dto.Products);
        }

        public void ValidateDateSpan(DateTime creationDate, DateTime deliveryDate)
        {
            if (creationDate > deliveryDate)
                throw new DtoValidationException(ApiErrorSlug.InvalidDateSpan);
        }

        public void ValidateProducts(List<TransactionProductDTO> products)
        {
            foreach (var product in products)
            {
                ValidateNumberIsPositive(product.Amount, "amount");
                ValidateNumberIsPositive(product.ProductBalanceId, "productBalanceId");
            }
        }
    }
}
