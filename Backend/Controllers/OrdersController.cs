using System;
using Backend.Models;
using Backend.Models.DTO;
using Backend.Models.OrderEntity.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ApiControllerBase
    {
        public OrdersController(ApiContext context) : base(context) { }

        [HttpPost]
        public ActionResult<GetObjectDTO<CreateOrderDTO>> CreateOrder([FromBody] CreateOrderDTO dto)
        {
            throw new NotImplementedException();
            

            return Ok(new GetObjectDTO<CreateOrderDTO>(dto));
        }
    }
}
