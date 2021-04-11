using System.Threading.Tasks;
using System.Linq;
using Backend.Models;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentTypesController: ApiControllerBase
    {
        public PaymentTypesController(ApiContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<GetEnumerableDTO>> GetPaymentTypes()
        {
            var types = await Context.PaymentTypes
                .Select(pt => new EnumDTO
                {
                    Id = (int)pt.Id,
                    Name = pt.Name
                })
                .ToListAsync();

            return Ok(new GetEnumerableDTO(types));
        }
    }
}
