using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PharmaciesController : ApiControllerBase
    {
        public PharmaciesController(ApiContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<GetPharmaciesDTO>> GetPharmacies()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var pharmacies = await Context.Pharmacies
                .Include(p => p.Registers)
                .Select(p => new PharmacyDTO(p))
                .ToListAsync();
            var dto = new GetPharmaciesDTO(pharmacies);

            return Ok(dto);
        }
    }
}
