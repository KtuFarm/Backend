using Backend.Exceptions;
using Backend.Models.Common;
using Backend.Models.OrderEntity.DTO;

namespace Backend.Services.Validators.OrderDTOValidator
{
    public class OrderDTOValidator : DTOValidator, IOrderDTOValidator
    {
        public void ValidateCreateOrderDto(CreateOrderDTO dto)
        {
            ValidateNumberIsPositive(dto.PharmacyId, "pharmacyId");
            ValidateNumberIsPositive(dto.WarehouseId, "warehouseId");

            if (dto.CreationDate > dto.DeliveryDate)
                throw new DtoValidationException(ApiErrorSlug.InvalidDateSpan);
        }
    }
}
