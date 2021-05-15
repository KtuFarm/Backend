using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.DTO;
using Backend.Models.OrderEntity;
using Backend.Models.OrderEntity.DTO;
using Backend.Models.ProductBalanceEntity;

namespace Backend.Services.OrdersManager
{
    public interface IOrdersManager
    {
        public Task TryCreateOrder(CreateOrderDTO dto, int pharmacyId);

        public Task<Order> GetOrder(int id);

        public Task<List<ProductBalance>> CreateProductBalanceList(IEnumerable<TransactionProductDTO> products);

        public Task UpdateOrder(IEnumerable<TransactionProductDTO> dto, Order order);

        public Task UpdateOrder(IEnumerable<TransactionProductDTO> dto, int orderId);
    }
}
