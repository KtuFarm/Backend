using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Exceptions;
using Backend.Models;
using Backend.Models.Common;
using Backend.Models.Database;
using Backend.Models.DTO;
using Backend.Models.OrderEntity;
using Backend.Models.OrderEntity.DTO;
using Backend.Models.ProductBalanceEntity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.OrdersManager
{
    public class OrdersManager : IOrdersManager
    {
        private readonly ApiContext _context;

        public OrdersManager(ApiContext context)
        {
            _context = context;
        }

        public async Task TryCreateOrder(CreateOrderDTO dto, int pharmacyId)
        {
            var order = IsOrderCreated(dto, pharmacyId);

            if (order != null)
            {
                throw new DuplicateObjectException("order", order.Id);
            }

            var productBalances = await CreateProductBalanceList(dto.Products);
            var newOrder = await CreateNewOrder(dto, productBalances, pharmacyId);

            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();
        }

        private Order IsOrderCreated(CreateOrderDTO dto, int pharmacyId)
        {
            return _context.Orders
                .AsEnumerable()
                .FirstOrDefault(o => o.IsCreatedToday(dto, pharmacyId));
        }

        public async Task<List<ProductBalance>> CreateProductBalanceList(IEnumerable<TransactionProductDTO> products)
        {
            var productBalances = new List<ProductBalance>();

            foreach (var product in products)
            {
                try
                {
                    var orderBalance = await CreateProductBalance(product);
                    productBalances.Add(orderBalance);
                }
                catch (NullReferenceException) { }
            }

            return productBalances;
        }

        private async Task<ProductBalance> CreateProductBalance(TransactionProductDTO product)
        {
            var productBalance = await _context.ProductBalances
                .Where(pb => pb.Id == product.ProductBalanceId)
                .Include(pb => pb.Medicament)
                .FirstOrDefaultAsync();

            if (productBalance == null) throw new NullReferenceException();

            return new ProductBalance(productBalance, product.Amount);
        }

        private async Task<Order> CreateNewOrder(CreateOrderDTO dto, IEnumerable<ProductBalance> productBalances,
            int pharmacyId)
        {
            string pharmacyAddress = await GetPharmacyAddress(pharmacyId);
            string warehouseAddress = await GetWarehouseAddress(dto.WarehouseId);

            return new Order(dto, pharmacyAddress, warehouseAddress, pharmacyId, productBalances);
        }

        private async Task<string> GetWarehouseAddress(int id)
        {
            string warehouseAddress = await _context.Warehouses
                .Where(w => w.Id == id)
                .Select(w => w.Address)
                .FirstOrDefaultAsync();

            if (warehouseAddress == null)
            {
                throw new ResourceNotFoundException(ApiErrorSlug.ResourceNotFound, "warehouseId");
            }

            return warehouseAddress;
        }

        private async Task<string> GetPharmacyAddress(int id)
        {
            string pharmacyAddress = await _context.Pharmacies
                .Where(p => p.Id == id)
                .Select(p => p.Address)
                .FirstOrDefaultAsync();

            if (pharmacyAddress == null)
            {
                throw new ResourceNotFoundException(ApiErrorSlug.ResourceNotFound, "pharmacyId");
            }

            return pharmacyAddress;
        }

        public async Task<Order> GetOrder(int id)
        {
            var order = await _context.Orders
                .Where(o => o.Id == id)
                .Include(o => o.OrderProductBalances)
                .FirstOrDefaultAsync();

            if (order == null) throw new ResourceNotFoundException("order");

            return order;
        }

        public async Task UpdateOrder(IEnumerable<TransactionProductDTO> dto, Order order)
        {
            if (order.OrderStateId > OrderStateId.Approved) throw new InvalidOperationException();
            var productBalances = await CreateProductBalanceList(dto);

            order.UpdateFromDTO(productBalances);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrder(IEnumerable<TransactionProductDTO> dto, int orderId)
        {
            await UpdateOrder(dto, await GetOrder(orderId));
        }
    }
}
