using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Models.Database;
using Backend.Models.DTO;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MedicamentsController : ApiControllerBase
    {
        public MedicamentsController(ApiContext context) : base(context) { }

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

            if (medicament == null) return ApiNotFound("Medicament does not exist!");

            return Ok(new GetMedicamentDTO(medicament));
        }

        [HttpPost]
        public async Task<ActionResult> AddMedicament([FromBody] CreateMedicamentDTO dto)
        {
            try
            {
                ValidateCreateMedicamentDTO(dto);
                await Context.Medicaments.AddAsync(new Medicament(dto));
                await Context.SaveChangesAsync();
            }
            catch (ArgumentException ex)
            {
                return ApiBadRequest("Invalid request body!", ex.Message);
            }
            
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditMedicament(int id, [FromBody] EditMedicamentDTO dto)
        {
            ValidateEditMedicamentDTO(dto);
            var medicament = await Context.Medicaments.FirstOrDefaultAsync(m => m.Id == id);

            if (medicament == null) return ApiNotFound("Medicament not found!");

            // medicament.IsPrescriptionRequired = (bool) dto.IsPrescriptionRequired;
            // medicament.IsReimbursed = (bool) dto.IsReimbursed;
            // medicament.BasePrice = (decimal) dto.BasePrice;
            // medicament.Surcharge = (double) dto.Surcharge;
            // medicament.IsSellable = (bool) dto.IsSellable;
            // medicament.ReimbursePercentage = (int) dto.ReimbursePercentage;

            await Context.SaveChangesAsync();

            return Ok();
        }

        [AssertionMethod]
        private static void ValidateCreateMedicamentDTO(CreateMedicamentDTO dto)
        {
            // if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentException("Name is empty!");
            // if (string.IsNullOrEmpty(dto.ActiveSubstance)) throw new ArgumentException("ActiveSubstance is empty!");
            // if (string.IsNullOrEmpty(dto.BarCode)) throw new ArgumentException("BarCode is empty!");
            // if (!dto.IsPrescriptionRequired.HasValue) throw new ArgumentException("IsPrescriptionRequired is empty!");
            // if (!dto.IsReimbursed.HasValue) throw new ArgumentException("IsReimbursed is empty!");
            // if (string.IsNullOrEmpty(dto.Country)) throw new ArgumentException("Country is empty!");
            // if (dto.BasePrice == null) throw new ArgumentException("BasePrice is empty!");
            // if (dto.Surcharge == null) throw new ArgumentException("Surcharge is empty!");
            // if (!dto.IsSellable.HasValue) throw new ArgumentException("IsSellable is empty!");
            // if (dto.ReimbursePercentage == null) throw new ArgumentException("ReimbursePercentage is empty!");
        }

        [AssertionMethod]
        private static void ValidateEditMedicamentDTO(EditMedicamentDTO dto)
        {
            if (!dto.IsPrescriptionRequired.HasValue) throw new ArgumentException("IsPrescriptionRequired is empty!");
            if (!dto.IsReimbursed.HasValue) throw new ArgumentException("IsReimbursed is empty!");
            if (dto.BasePrice == null) throw new ArgumentException("BasePrice is empty!");
            if (dto.Surcharge == null) throw new ArgumentException("Surcharge is empty!");
            if (!dto.IsSellable.HasValue) throw new ArgumentException("IsSellable is empty!");
            if (dto.ReimbursePercentage == null) throw new ArgumentException("ReimbursePercentage is empty!");
        }
    }
}
