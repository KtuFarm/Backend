using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Exceptions;
using Backend.Models;
using Backend.Models.Database;
using Backend.Models.DTO;
using Backend.Models.OrderEntity;
using Backend.Models.OrderEntity.DTO;
using Backend.Models.ProductBalanceEntity;

namespace Backend.Services.OrdersManager
{
    public class OrdersManager : IOrdersManager
    {
        private readonly ApiContext _context;

        public OrdersManager(ApiContext context)
        {
            _context = context;
        }

        public bool IsOrderCreated(Order order, CreateOrderDTO dto, int ? pharmacyId)
        {

            return _context.Orders
                .AsEnumerable()
                .Any(o => IsOrderCreatedToday(o, dto, pharmacyId));
        }

        public async Task TryCreateOrder(CreateOrderDTO dto, int pharmacyId)
        {
            bool orderExists = IsOrderCreated(dto, pharmacyId);

            if (orderExists)
            {
                throw new DuplicateObjectException("order");
            }

            var productBalances = await GetOrderProductBalances(dto.Products);
            var order = await CreateNewOrder(dto, productBalances, pharmacyId);

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public void AggregateOrders()
        {
            throw new NotImplementedException();
        }

        public void AgregateOrders()
        {
            throw new NotImplementedException();
        }

        private bool IsOrderCreatedToday(Order order, CreateOrderDTO dto, int? pharmacyId)
        {
            return order.WarehouseId == dto.WarehouseId
                   && order.PharmacyId == pharmacyId
                   && order.OrderStateId == OrderStateId.Created
                   && order.CreationDate.Date == DateTime.Now.Date;
        }

        private async Task<List<ProductBalance>> GetOrderProductBalances(IEnumerable<TransactionProductDTO> products)
        {
            var productBalances = new List<ProductBalance>();

            foreach (var product in products)
            {
                var productBalance = await Context.ProductBalances
                    .FirstOrDefaultAsync(pb => pb.Id == product.ProductBalanceId);

                if (productBalance == null) continue;

                var orderProductBalance = new ProductBalance(productBalance, product.Amount);
                productBalances.Add(orderProductBalance);
            }

            return productBalances;
        }
    }
}
