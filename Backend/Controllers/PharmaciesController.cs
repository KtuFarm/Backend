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
            if (!IsValidApiRequest()) return ApiBadRequest("Invalid Headers!");

            var pharmacies = await Context.Pharmacies
                .Include(p => p.Registers)
                .Select(p => new PharmacyDTO(p))
                .ToListAsync();
            var dto = new GetPharmaciesDTO(pharmacies);

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult CreatePharmacy(CreatePharmacyDTO dataFromBody)
        {
            if (!IsValidApiRequest()) return ApiBadRequest("Invalid Headers!");

            var workingHours = _workingHoursManager.GetWorkingHoursFromDTO(dataFromBody.WorkingHours);
  
            return StatusCode(500);
            // return Created();
        }
    }
}
