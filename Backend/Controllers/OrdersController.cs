using System.Linq;
using System.Threading.Tasks;
using Backend.Exceptions;
using Backend.Models;
using Backend.Models.DTO;
using Backend.Models.OrderEntity;
using Backend.Models.OrderEntity.DTO;
using Backend.Services.OrderManager;
using Backend.Services.Validators.OrderDTOValidator;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ApiControllerBase
    {
        private readonly IOrderManager _orderManager;
        private readonly IOrderDTOValidator _orderDtoValidator;

        public OrdersController(ApiContext context, IOrderManager orderManager, IOrderDTOValidator orderDtoValidator) : base(context)
        {
            _orderManager = orderManager;
            _orderDtoValidator = orderDtoValidator;
        }

        [HttpPost]
        public async Task<ActionResult<GetObjectDTO<CreateOrderDTO>>> CreateOrder([FromBody] CreateOrderDTO dto)
        {
            try
            {
                _orderDtoValidator.ValidateCreateOrderDto(dto);
            }
            catch(DtoValidationException ex)
            {
                return ApiBadRequest(ex.Message, ex.Parameter);
            }

            var orderFromDatabase = Context.Orders.FirstOrDefault(o => o.WarehouseId == dto.WarehouseId && o.PharmacyId == dto.PharmacyId);

            if (orderFromDatabase == null)
            {
                var pharmacy = Context.Pharmacies.FirstOrDefault(p => p.Id == dto.PharmacyId);
                if (pharmacy == null) return BadRequest("Specified pharmacy does not exist.");

                var warehouse = Context.Warehouses.FirstOrDefault(w => w.Id == dto.WarehouseId);
                if (warehouse == null) return BadRequest("Specified warehouse does not exist.");

                var order = new Order(dto, pharmacy.Address, warehouse.Address);
                await Context.Orders.AddAsync(order);
            }
            else orderFromDatabase.UpdateFromDTO(dto);

            await Context.SaveChangesAsync();
            return Ok(new GetObjectDTO<CreateOrderDTO>(dto));
        }
    }
}
