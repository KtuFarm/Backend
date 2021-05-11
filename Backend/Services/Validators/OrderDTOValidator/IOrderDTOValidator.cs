using Backend.Models.OrderEntity.DTO;
using JetBrains.Annotations;

namespace Backend.Services.Validators.OrderDTOValidator
{
    public interface IOrderDTOValidator
    {
        [AssertionMethod]
        public void ValidateCreateOrderDto(CreateOrderDTO dto);
    }
}
