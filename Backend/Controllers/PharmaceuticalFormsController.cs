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
    public class PharmaceuticalFormsController: ApiControllerBase
    {
        public PharmaceuticalFormsController(ApiContext context) : base(context) { }

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
