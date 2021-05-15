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
using Backend.Models.UserEntity;
using Backend.Services.Validators.OrderDTOValidator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ApiControllerBase
    {
        private readonly IOrderDTOValidator _orderDtoValidator;
        private const string ModelName = "order";

        public OrdersController(ApiContext context,
            IOrderDTOValidator orderDtoValidator,
            UserManager<User> userManager)
            : base(context, userManager)
        {
            _orderDtoValidator = orderDtoValidator;
        }

        [HttpGet]
        [Authorize(Roles = "Pharmacy, Warehouse")]
        public async Task<ActionResult<GetListDTO<OrderDTO>>> GetOrders()
        {
            var user = await GetCurrentUser();

            var orders = await GetUserOrdersQuery(user)
                .Select(o => new OrderDTO(o))
                .ToListAsync();

            return Ok(new GetListDTO<OrderDTO>(orders));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Pharmacy, Warehouse")]
        public async Task<ActionResult<GetObjectDTO<OrderFullDTO>>> GetOrder(int id)
        {
            var user = await GetCurrentUser();

            var order = await Context.Orders
                .Where(o => o.Id == id)
                .Include(o => o.OrderProductBalances)
                .ThenInclude(opb => opb.ProductBalance)
                .ThenInclude(pb => pb.Medicament)
                .FirstOrDefaultAsync();

            if (order == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);
            if (!order.IsAuthorized(user)) return ApiUnauthorized();

            var dto = new OrderFullDTO(order);
            return Ok(new GetObjectDTO<OrderFullDTO>(dto));
        }

        [HttpPost]
        [Authorize(Roles = "Pharmacy")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO dto)
        {
            try
            {
                _orderDtoValidator.ValidateCreateOrderDto(dto);

                var user = await GetCurrentUser();
                if (user.PharmacyId == null) return ApiUnauthorized();
                int pharmacyId = (int) user.PharmacyId;

                bool orderExists = IsOrderCreated(dto, pharmacyId);

                if (orderExists) return ApiBadRequest(ApiErrorSlug.ObjectAlreadyExists, ModelName);

                var productBalances = await GetOrderProductBalances(dto.Products);
                var order = await CreateNewOrder(dto, productBalances, pharmacyId);

                await Context.Orders.AddAsync(order);
                await Context.SaveChangesAsync();

                return Created();
            }
            catch (DtoValidationException ex)
            {
                return ApiBadRequest(ex.Message, ex.Parameter);
            }
            catch (ResourceNotFoundException ex)
            {
                return ApiNotFound(ex.Message, ex.Parameter);
            }
        }

        private bool IsOrderCreated(CreateOrderDTO dto, int pharmacyId)
        {
            return Context.Orders
                .AsEnumerable()
                .Any(o => IsOrderCreatedToday(o, dto, pharmacyId));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Pharmacy")]
        public async Task<IActionResult> EditOrder(int id, [FromBody] TransactionProductDTO[] dto)
        {
            var order = await Context.Orders
                .Where(o => o.Id == id)
                .Include(o => o.OrderProductBalances)
                .FirstOrDefaultAsync();

            if (order == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

            var user = await GetCurrentUser();

            if (!user.IsAuthorizedToEdit(order)) return ApiUnauthorized();

            var productBalances = await GetOrderProductBalances(dto);

            order.OrderProductBalances = productBalances
                .Select(pb => new OrderProductBalance(order, pb))
                .ToList();

            await Context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{id}/cancel")]
        [Authorize(Roles = "Pharmacy")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var order = await Context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

            var user = await GetCurrentUser();
            if (order.PharmacyId != user.PharmacyId) return ApiUnauthorized();
            if (order.OrderStateId > OrderStateId.Approved) return ApiBadRequest(ApiErrorSlug.InvalidStatus, ModelName);

            order.OrderStateId = OrderStateId.Canceled;

            await Context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{id}/approve")]
        [Authorize(Roles = "Pharmacy")]
        public async Task<IActionResult> ApproveOrder(int id)
        {
            var user = await GetCurrentUser();
            var order = Context.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);
            if (order.PharmacyId != user.PharmacyId) return ApiUnauthorized();

            order.OrderStateId = OrderStateId.Approved;

            await Context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{id}/prepare")]
        [Authorize(Roles = "Warehouse")]
        public async Task<IActionResult> PrepareOrder(int id)
        {
            var user = await GetCurrentUser();
            var order = await Context.Orders
                .Where(o => o.Id == id)
                .Include(o => o.OrderProductBalances)
                .ThenInclude(opb => opb.ProductBalance)
                .FirstOrDefaultAsync();

            if (order == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);
            if (order.WarehouseId != user.WarehouseId) return ApiUnauthorized();
            if (order.OrderStateId != OrderStateId.Approved)
            {
                return ApiBadRequest(ApiErrorSlug.InvalidStatus, ModelName);
            }

            var warehouseProducts = Context.ProductBalances.Where(pb => pb.WarehouseId == user.WarehouseId);

            foreach (var opb in order.OrderProductBalances)
            {
                var orderProduct = opb.ProductBalance;
                var productBalance = await warehouseProducts.FirstOrDefaultAsync(pb =>
                    pb.MedicamentId == orderProduct.MedicamentId && pb.ExpirationDate == orderProduct.ExpirationDate);

                if (productBalance == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, "productBalance");
                if (productBalance.Amount < orderProduct.Amount)
                {
                    return ApiBadRequest(ApiErrorSlug.InsufficientBalance, "productBalance");
                }

                productBalance.Amount -= orderProduct.Amount;
            }

            order.OrderStateId = OrderStateId.Ready;
            await Context.SaveChangesAsync();

            return Ok();
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

        private static bool IsOrderCreatedToday(Order order, CreateOrderDTO dto, int? pharmacyId)
        {
            return order.WarehouseId == dto.WarehouseId
                   && order.PharmacyId == pharmacyId
                   && order.OrderStateId == OrderStateId.Created
                   && order.CreationDate.Date == DateTime.Now.Date;
        }

        private async Task<Order> CreateNewOrder(
            CreateOrderDTO dto,
            IEnumerable<ProductBalance> productBalances,
            int pharmacyId)
        {
            string pharmacyAddress = await GetPharmacyAddress(pharmacyId);
            string warehouseAddress = await GetWarehouseAddress(dto.WarehouseId);

            return new Order(dto, pharmacyAddress, warehouseAddress, pharmacyId, productBalances);
        }

        private async Task<string> GetWarehouseAddress(int id)
        {
            string warehouseAddress = await Context.Warehouses
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
            string pharmacyAddress = await Context.Pharmacies
                .Where(p => p.Id == id)
                .Select(p => p.Address)
                .FirstOrDefaultAsync();

            if (pharmacyAddress == null)
            {
                throw new ResourceNotFoundException(ApiErrorSlug.ResourceNotFound, "pharmacyId");
            }

            return pharmacyAddress;
        }

        private IQueryable<Order> GetUserOrdersQuery(User user)
        {
            return user.DepartmentId switch
            {
                DepartmentId.Pharmacy => Context.Orders.Where(o => o.PharmacyId == user.PharmacyId),
                DepartmentId.Warehouse => Context.Orders.Where(o => o.WarehouseId == user.WarehouseId),
                _ => null
            };
        }
    }
}
