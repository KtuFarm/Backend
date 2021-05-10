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
    public class PharmaceuticalFormsController: ApiControllerBase
    {
        public PharmaceuticalFormsController(ApiContext context, UserManager<User> userManager) : base(context, userManager) { }

        [HttpGet]
        public async Task<ActionResult<GetListDTO<EnumDTO>>> GetPharmaceuticalForm()
        {
            var forms = await Context.PharmaceuticalForms
                .Select(pf => new EnumDTO
                {
                    Id = (int)pf.Id,
                    Name = pf.Name
                })
                .ToListAsync();

            return Ok(new GetListDTO<EnumDTO>(forms));
        }
    }
}
