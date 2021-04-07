﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Models.DTO;
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

        //[HttpPost]
        //public async Task<ActionResult> AddMedicament([FromBody] CreateMedicamentDTO dataFromBody)
        //{
        //    if (!IsValidApiRequest()) return InvalidHeaders();

        //    var pharmaceuticalForm = Context.PharmaceuticalForms
        //        .FirstOrDefaultAsync(p => (int) p.Id == dataFromBody.PharmaceuticalFormId);

<<<<<<< Updated upstream

        //}
=======
        }
>>>>>>> Stashed changes
    }
}
