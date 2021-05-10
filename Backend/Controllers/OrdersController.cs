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
using Backend.Services.Validators.OrderDTOValidator;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ApiControllerBase
    {
        private readonly IOrderDTOValidator _orderDtoValidator;

        public OrdersController(ApiContext context, IOrderDTOValidator orderDtoValidator) : base(context)
        {
            _orderDtoValidator = orderDtoValidator;
        }

        [HttpPost]
        public async Task<ActionResult<GetObjectDTO<CreateOrderDTO>>> CreateOrder([FromBody] CreateOrderDTO dto)
        {
            try
            {
                _orderDtoValidator.ValidateCreateOrderDto(dto);

                var orderFromDatabase = Context.Orders.FirstOrDefault(o => IsOrderCreatedToday(o, dto));

                if (orderFromDatabase == null)
                {
                    var order = CreateNewOrder(dto);
                    await Context.Orders.AddAsync(order);
                }
                else orderFromDatabase.UpdateFromDTO(dto);

                await Context.SaveChangesAsync();
                return Ok(new GetObjectDTO<CreateOrderDTO>(dto));
            }
            catch(DtoValidationException ex)
            {
                return ApiBadRequest(ex.Message, ex.Parameter);
            }
            catch(ResourceNotFoundException ex)
            {
                return ApiNotFound(ex.Message, ex.Parameter);
            }
        }

        private bool IsOrderCreatedToday(Order order, CreateOrderDTO dto)
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
    }
}
