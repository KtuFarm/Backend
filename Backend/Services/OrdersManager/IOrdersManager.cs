using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.DTO;
using Backend.Models.OrderEntity;
using Backend.Models.OrderEntity.DTO;

namespace Backend.Services.OrdersManager
{
    public interface IOrdersManager
    {
        public Task TryCreateOrder(CreateOrderDTO dto, int pharmacyId);

        public Task<Order> GetOrder(int id);

        public Task UpdateOrder(IEnumerable<TransactionProductDTO> dto, Order order);

        public void AggregateOrders();
    }
}
