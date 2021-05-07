using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Backend.Exceptions;
using Backend.Models;
using Backend.Models.Common;
using Backend.Models.DTO;
using Backend.Models.MedicamentEntity;
using Backend.Models.MedicamentEntity.DTO;
using Backend.Services.Validators.MedicamentDTOValidator;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Backend.Models.UserEntity;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MedicamentsController : ApiControllerBase
    {
        private const string ModelName = "medicament";
        private readonly IMedicamentDTOValidator _validator;

        public MedicamentsController(ApiContext context, IMedicamentDTOValidator validator, UserManager<User> userManager) : base(context, userManager)
        {
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<GetListDTO<MedicamentDTO>>> GetMedicaments()
        {
            var medicaments = await Context.Medicaments
                .Select(m => new MedicamentDTO(m))
                .ToListAsync();

            return Ok(new GetListDTO<MedicamentDTO>(medicaments));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetObjectDTO<MedicamentFullDTO>>> GetMedicament(int id)
        {
            var medicament = await Context.Medicaments
                .Where(m => m.Id == id)
                .Include(m => m.PharmaceuticalForm)
                .Select(m => new MedicamentFullDTO(m))
                .FirstOrDefaultAsync();

            if (medicament == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

            return Ok(new GetObjectDTO<MedicamentFullDTO>(medicament));
        }

        [HttpPost]
        public async Task<ActionResult> CreateMedicament([FromBody] CreateMedicamentDTO dto)
        {
            try
            {
                _validator.ValidateCreateMedicamentDto(dto);
                await Context.Medicaments.AddAsync(new Medicament(dto));
                await Context.SaveChangesAsync();
            }
            catch (DtoValidationException ex)
            {
                return ApiBadRequest(ex.Message, ex.Parameter);
            }

            return Created();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditMedicament(int id, [FromBody] EditMedicamentDTO dto)
        {
            try
            {
                _validator.ValidateEditMedicamentDto(dto);

                var medicament = await Context.Medicaments.FirstOrDefaultAsync(m => m.Id == id);
                if (medicament == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

                medicament.UpdateFromDTO(dto);

                await Context.SaveChangesAsync();
            }
            catch (DtoValidationException ex)
            {
                return ApiBadRequest(ex.Message, ex.Parameter);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMedicament(int id)
        {
            var medicament = await Context.Medicaments
                .FirstOrDefaultAsync(p => p.Id == id);

            if (medicament == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

            medicament.IsSoftDeleted = true;
            await Context.SaveChangesAsync();

            return Ok();
        }
    }
}
