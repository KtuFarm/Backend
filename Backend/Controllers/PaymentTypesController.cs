using System.Threading.Tasks;
using System.Linq;
using Backend.Models;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Backend.Models.UserEntity;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentTypesController: ApiControllerBase
    {
        public PaymentTypesController(ApiContext context, UserManager<User> userManager) : base(context, userManager) { }

        [HttpGet]
        public async Task<ActionResult<GetListDTO<EnumDTO>>> GetPaymentTypes()
        {
            var types = await Context.PaymentTypes
                .Select(pt => new EnumDTO
                {
                    Id = (int)pt.Id,
                    Name = pt.Name
                })
                .ToListAsync();

            return Ok(new GetListDTO<EnumDTO>(types));
        }
    }
}
