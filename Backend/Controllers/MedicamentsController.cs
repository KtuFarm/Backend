using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Backend.Exceptions;
using Backend.Models;
using Backend.Models.Database;
using Backend.Models.DTO;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MedicamentsController : ApiControllerBase
    {
        private readonly IMedicamentDTOValidator _validator;

        public MedicamentsController(ApiContext context, IMedicamentDTOValidator validator) : base(context)
        {
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<GetMedicamentsDTO>> GetMedicaments()
        {
            var medicaments = await Context.Medicaments
                .Select(m => new MedicamentDTO(m))
                .ToListAsync();

            return Ok(new GetMedicamentsDTO(medicaments));
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<GetMedicamentDTO>> GetMedicament(int id)
        {
            var medicament = await Context.Medicaments
                .Include(m => m.PharmaceuticalForm)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medicament == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, "medicament");

            return Ok(new GetMedicamentDTO(medicament));
        }

        [HttpPost]
        public async Task<ActionResult> AddMedicament([FromBody] CreateMedicamentDTO dto)
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
                if (medicament == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, "medicament");
                
                medicament.UpdateMedicamentFromDTO(dto);
                
                await Context.SaveChangesAsync();
            }
            catch (DtoValidationException ex)
            {
                return ApiBadRequest(ex.Message, ex.Parameter);
            }
            
            return Ok();
        }
    }
}
