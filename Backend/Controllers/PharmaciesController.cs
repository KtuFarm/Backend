using Backend.Models;
using Backend.Models.Database;
using Backend.Models.DTO;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

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
            var pharmacies = await Context.Pharmacies
                .Select(p => new PharmacyDTO(p))
                .ToListAsync();

            return Ok(new GetPharmaciesDTO(pharmacies));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPharmacyDTO>> GetPharmacy(int id)
        {
            var pharmacy = await Context.Pharmacies
                .Include(p => p.PharmacyWorkingHours)
                .FirstOrDefaultAsync(p => p.Id == id);

            var workingHours = await _workingHoursManager.GetPharmacyWorkingHours(pharmacy.Id);

            return Ok(new GetPharmacyDTO(pharmacy, workingHours));
        }

        [HttpPost]
        public async Task<ActionResult> CreatePharmacy([FromBody] CreatePharmacyDTO dataFromBody)
        {
            List<WorkingHours> workingHours;
            try
            {
                ValidateCreatePharmacyDTO(dataFromBody);
                workingHours = _workingHoursManager.GetWorkingHoursFromDTO(dataFromBody.WorkingHours);
            }
            catch (ArgumentException ex)
            {
                return ApiBadRequest("Invalid request body!", ex.Message);
            }

            var pharmacy = new Pharmacy(dataFromBody, workingHours);
            
            CreateRegisters(dataFromBody.RegistersCount, pharmacy);
            Context.Add(pharmacy);
            
            await Context.SaveChangesAsync();

            return Created();
        }

        [AssertionMethod]
        private static void ValidateCreatePharmacyDTO(CreatePharmacyDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Address)) throw new ArgumentException("Address is empty!");
            if (string.IsNullOrEmpty(dto.City)) throw new ArgumentException("City is empty!");
            if (dto.RegistersCount < 1) throw new ArgumentException("There should be at least 1 register!");
        }

        private void CreateRegisters(int count, Pharmacy pharmacy)
        {
            for (int i = 0; i < count; i++)
            {
                Context.Add(new Register(pharmacy));
            }
        }
    }
}
