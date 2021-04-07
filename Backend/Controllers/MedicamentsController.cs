using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public MedicamentsController(ApiContext context) : base(context)
        {
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetMedicamentDTO>> GetMedicament(int id)
        {
            if (!IsValidApiRequest()) return InvalidHeaders();

            var medicament = await Context.Medicaments
                .Include(m => m.PharmaceuticalForm)
                .FirstOrDefaultAsync(m => m.Id == id);

            return Ok(new GetMedicamentDTO(medicament));
        }

        [HttpGet]
        public async Task<ActionResult<GetMedicamentsDTO>> GetMedicaments()
        {
            if (!IsValidApiRequest()) return InvalidHeaders();

            var medicaments = await Context.Medicaments
                .Include(m => m.PharmaceuticalForm)
                .Select(m => new MedicamentDTO(m))
                .ToListAsync();

            return Ok(new GetMedicamentsDTO(medicaments));
        }

        [HttpPost]
        public async Task<ActionResult> AddMedicament([FromBody] CreateMedicamentDTO dataFromBody)
        {
            ValidateCreateMedicamentDTO(dataFromBody);

            var pharmaceuticalForm = Context.PharmaceuticalForms
                .FirstOrDefaultAsync(p => (int)p.Id == dataFromBody.PharmaceuticalFormId);

            Context.Medicaments.Add(new Medicament(dataFromBody, await pharmaceuticalForm));
            await Context.SaveChangesAsync();

            return Created();
        }

        [AssertionMethod]
        private static void ValidateCreateMedicamentDTO(CreateMedicamentDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentException("Name is empty!");
            if (string.IsNullOrEmpty(dto.ActiveSubstance)) throw new ArgumentException("Active substance is empty!");
            if (string.IsNullOrEmpty(dto.BarCode)) throw new ArgumentException("Bar code is empty!");
            if (!dto.IsPrescriptionRequired.HasValue) throw new ArgumentException("Is prescription required is empty!");
            if (!dto.IsReimbursed.HasValue) throw new ArgumentException("Is prescription required is empty!");
            if (string.IsNullOrEmpty(dto.Country)) throw new ArgumentException("Country is empty!");
            if (dto.BasePrice == null) throw new ArgumentException("Base price is empty!");
            if (dto.Surcharge == null) throw new ArgumentException("Surcharge is empty!");
            if (!dto.IsSellable.HasValue) throw new ArgumentException("Is sellable is empty!");
            if (dto.ReimbursePercentage == null) throw new ArgumentException("Reimburse percentage is empty!");
        }
    }
}
