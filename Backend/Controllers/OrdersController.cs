using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Exceptions;
using Backend.Models;
using Backend.Models.Common;
using Backend.Models.Database;
using Backend.Models.DTO;
using Backend.Models.OrderEntity;
using Backend.Models.OrderEntity.DTO;
using Backend.Models.UserEntity;
using Backend.Services.OrdersManager;
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
        private readonly IOrdersManager _ordersManager;
        private const string ModelName = "order";

        public OrdersController(ApiContext context,
            IOrderDTOValidator orderDtoValidator,
            IOrdersManager ordersManager,
            UserManager<User> userManager)
            : base(context, userManager)
        {
            _orderDtoValidator = orderDtoValidator;
            _ordersManager = ordersManager;
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

                await _ordersManager.TryCreateOrder(dto, pharmacyId);

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
            catch (DuplicateObjectException ex)
            {
                return ApiBadRequest(ApiErrorSlug.ObjectAlreadyExists, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Pharmacy")]
        public async Task<IActionResult> EditOrder(int id, [FromBody] TransactionProductDTO[] dto)
        {
            try
            {
                var order = await _ordersManager.GetOrder(id);

                var user = await GetCurrentUser();
                if (!user.IsAuthorizedToEdit(order)) return ApiUnauthorized();

                await _ordersManager.UpdateOrder(dto, order);

                return Ok();
            }
            catch (ResourceNotFoundException ex)
            {
                return ApiNotFound(ApiErrorSlug.ResourceNotFound, ex.Message);
            }
            catch (InvalidOperationException)
            {
                return ApiBadRequest(ApiErrorSlug.InvalidStatus, ModelName);
            }
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
