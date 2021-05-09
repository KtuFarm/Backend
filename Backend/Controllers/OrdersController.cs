using System;
using Backend.Models;
using Backend.Models.DTO;
using Backend.Models.OrderEntity.DTO;
using Backend.Services.OrderManager;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ApiControllerBase
    {
        private readonly IOrderManager _orderManager;

        public OrdersController(ApiContext context, IOrderManager orderManager) : base(context)
        {
            _orderManager = orderManager;
        }

        [HttpPost]
        public ActionResult<GetObjectDTO<CreateOrderDTO>> CreateOrder([FromBody] CreateOrderDTO dto)
        {

            return Ok(new GetObjectDTO<CreateOrderDTO>(dto));
        }
    }
}
