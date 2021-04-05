using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Models.Database;
using Backend.Models.DTO;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PharmaciesController : ApiControllerBase
    {
        private readonly IWorkingHoursManager _workingHoursManager;

        public PharmaciesController(ApiContext context, IWorkingHoursManager workingHoursManager) : base(context)
        {
            _workingHoursManager = workingHoursManager;
        }

        [HttpGet]
        public async Task<ActionResult<GetPharmaciesDTO>> GetPharmacies()
        {
            if (!IsValidApiRequest()) return InvalidHeaders();

            var pharmacies = await Context.Pharmacies
                .Select(p => new PharmacyDTO(p))
                .ToListAsync();
            
            return Ok(new GetPharmaciesDTO(pharmacies));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPharmacyDTO>> GetPharmacy(int id)
        {
            if (!IsValidApiRequest()) return InvalidHeaders();

            var pharmacy = await Context.Pharmacies
                .Include(p => p.PharmacyWorkingHours)
                .FirstOrDefaultAsync(p => p.Id == id);

            var workingHours = await _workingHoursManager.GetPharmacyWorkingHours(pharmacy.Id);
            
            return Ok(new GetPharmacyDTO(pharmacy, workingHours));
        }

        [HttpPost]
        public async Task<ActionResult> CreatePharmacy([FromBody] CreatePharmacyDTO dataFromBody)
        {
            if (!IsValidApiRequest()) return InvalidHeaders();

            List<WorkingHours> workingHours;
            try
            {
                workingHours = _workingHoursManager.GetWorkingHoursFromDTO(dataFromBody.WorkingHours);
            }
            catch (ArgumentException ex)
            {
                return ApiBadRequest("Invalid request body!", ex.Message);
            }

            Context.Add(new Pharmacy(dataFromBody, workingHours));
            await Context.SaveChangesAsync();
            
            return Created();
        }
    }
}
