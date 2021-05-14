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

            if (order == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, "order");
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

                //var orderFromDatabase = Context.Orders.AsEnumerable().FirstOrDefault(o => IsOrderCreatedToday(o, dto));

                //if (orderFromDatabase == null)
                //{
                var user = await GetCurrentUser();
                if (user.PharmacyId == null) return ApiUnauthorized();

                var productBalances = await GetOrderProductBalances(dto);
                var order = await CreateNewOrder(dto, productBalances, (int) user.PharmacyId);

                await Context.Orders.AddAsync(order);


                //}
                //else orderFromDatabase.UpdateFromDTO(dto);

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

        [HttpPost("{id}/approve")]
        [Authorize(Roles = "Pharmacy")]
        public async Task<IActionResult> ApproveOrder(int id)
        {
            var user = await GetCurrentUser();
            var order = Context.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, "order");
            if (order.PharmacyId != user.PharmacyId) return ApiUnauthorized();

            order.OrderStateId = OrderStateId.Approved;

            await Context.SaveChangesAsync();
            return Ok();
        }

        private async Task<List<ProductBalance>> GetOrderProductBalances(CreateOrderDTO dto)
        {
            var productBalances = new List<ProductBalance>();

            foreach (var product in dto.Products)
            {
                var productBalance = await Context.ProductBalances
                    .FirstOrDefaultAsync(pb => pb.Id == product.ProductBalanceId);

                if (productBalance == null) continue;

                var orderProductBalance = new ProductBalance(productBalance, product.Amount);
                productBalances.Add(orderProductBalance);
            }

            return productBalances;
        }

        private static bool IsOrderCreatedToday(Order order, CreateOrderDTO dto)
        {
            return order.WarehouseId == dto.WarehouseId
                   //  && order.PharmacyId == dto.PharmacyId
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
