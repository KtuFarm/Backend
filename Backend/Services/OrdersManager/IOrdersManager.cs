using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.OrderEntity;
using Backend.Models.OrderEntity.DTO;

namespace Backend.Services.OrdersManager
{
    public interface IOrdersManager
    {
        public bool IsOrderCreated(Order order, CreateOrderDTO dto, int? pharmacyId);
        public Task TryCreateOrder(CreateOrderDTO dto, int pharmacyId);
        public void AggregateOrders();
    }
}
