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
        public async Task<IActionResult> GetOrders()
        {
            var user = await GetCurrentUser();

            var orders = await GetUserOrdersQuery(user)
                .Select(o => new OrderDTO(o))
                .ToListAsync();

            return Ok(new GetListDTO<OrderDTO>(orders));
        }

        [HttpPost]
        public async Task<ActionResult<GetObjectDTO<CreateOrderDTO>>> CreateOrder([FromBody] CreateOrderDTO dto)
        {
            try
            {
                _orderDtoValidator.ValidateCreateOrderDto(dto);

                var orderFromDatabase = Context.Orders.AsEnumerable().FirstOrDefault(o => IsOrderCreatedToday(o, dto));

                if (orderFromDatabase == null)
                {
                    var order = CreateNewOrder(dto);
                    await Context.Orders.AddAsync(order);
                }
                else orderFromDatabase.UpdateFromDTO(dto);

                await Context.SaveChangesAsync();
                return Ok(new GetObjectDTO<CreateOrderDTO>(dto));
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

        private static bool IsOrderCreatedToday(Order order, CreateOrderDTO dto)
        {
            return order.WarehouseId == dto.WarehouseId
                   && order.PharmacyId == dto.PharmacyId
                   && order.OrderStateId == OrderStateId.Created
                   && order.CreationDate.Date == DateTime.Now.Date;
        }

        private Order CreateNewOrder(CreateOrderDTO dto)
        {
            var pharmacy = Context.Pharmacies.FirstOrDefault(p => p.Id == dto.PharmacyId);
            if (pharmacy == null) throw new ResourceNotFoundException(ApiErrorSlug.ResourceNotFound, "pharmacyId");

            var warehouse = Context.Warehouses.FirstOrDefault(w => w.Id == dto.WarehouseId);
            if (warehouse == null) throw new ResourceNotFoundException(ApiErrorSlug.ResourceNotFound, "warehouseId");

            return new Order(dto, pharmacy.Address, warehouse.Address);
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
