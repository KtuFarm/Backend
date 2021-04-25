using Backend.Models;
using Backend.Models.Database;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Exceptions;
using Backend.Models.Common;
using Backend.Models.Common.DTO;
using Backend.Services.Validators.PharmacyDTOValidator;
using Backend.Services.WorkingHoursManager;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PharmaciesController : ApiControllerBase
    {
        private const string ModelName = "pharmacy";

        private readonly IWorkingHoursManager _workingHoursManager;
        private readonly IPharmacyDTOValidator _validator;

        public PharmaciesController(
            ApiContext context,
            IWorkingHoursManager workingHoursManager,
            IPharmacyDTOValidator validator
        ) : base(context)
        {
            _workingHoursManager = workingHoursManager;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<GetListDTO<PharmacyDTO>>> GetPharmacies()
        {
            var pharmacies = await Context.Pharmacies
                .Select(p => new PharmacyDTO(p))
                .ToListAsync();

            return Ok(new GetListDTO<PharmacyDTO>(pharmacies));
        }

        [HttpGet("all")]
        public async Task<ActionResult<GetListDTO<PharmacyDTO>>> GetAllPharmacies()
        {
            var pharmacies = await Context.Pharmacies
                .IgnoreQueryFilters()
                .Select(p => new PharmacyDTO(p))
                .ToListAsync();

            return Ok(new GetListDTO<PharmacyDTO>(pharmacies));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPharmacyDTO>> GetPharmacy(int id)
        {
            var pharmacy = await Context.Pharmacies
                .Include(p => p.Registers)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pharmacy == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

            var workingHours = await _workingHoursManager.GetPharmacyWorkingHours(pharmacy.Id);

            return Ok(new GetPharmacyDTO(pharmacy, workingHours));
        }

        [HttpPost]
        public async Task<ActionResult> CreatePharmacy([FromBody] CreatePharmacyDTO dto)
        {
            List<WorkingHours> workingHours;
            try
            {
                _validator.ValidateCreatePharmacyDTO(dto);
                workingHours = _workingHoursManager.GetWorkingHoursFromDTO(dto.WorkingHours);
            }
            catch (DtoValidationException ex)
            {
                return ApiBadRequest(ex.Message, ex.Parameter);
            }

            Context.Add(new Pharmacy(dto, workingHours));
            await Context.SaveChangesAsync();

            return Created();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> EditPharmacy(int id, [FromBody] EditPharmacyDTO dto)
        {
            try
            {
                _validator.ValidateEditPharmacyDTO(dto);

                var pharmacy = await Context.Pharmacies
                    .Include(p => p.PharmacyWorkingHours)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (pharmacy == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

                var workingHours = _workingHoursManager.GetWorkingHoursFromDTO(dto.WorkingHours);
                pharmacy.UpdateFromDTO(dto, workingHours);

                await Context.SaveChangesAsync();
            }
            catch (DtoValidationException ex)
            {
                return ApiBadRequest(ex.Message, ex.Parameter);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePharmacy(int id)
        {
            var pharmacy = await Context.Pharmacies
                .Include(p => p.PharmacyWorkingHours)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pharmacy == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

            pharmacy.IsSoftDeleted = true;
            await Context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}/products")]
        public async Task<ActionResult<GetListDTO<ProductBalanceDTO>>> GetProductBalances(int id)
        {
            var products = await Context.ProductBalances
                .Include(pb => pb.Medicament)
                .Where(pb => pb.PharmacyId == id)
                .Select(p => new ProductBalanceDTO(p))
                .ToListAsync();

            return Ok(new GetListDTO<ProductBalanceDTO>(products));
        }

        [HttpGet("{id}/transactions")]
        public async Task<ActionResult<GetListDTO<TransactionDTO>>> GetTransactions(int id)
        {
            var transactions = await Context.Transactions
                .Where(t => t.PharmacyId == id)
                .Select(t => new TransactionDTO(t))
                .ToListAsync();

            return Ok(new GetListDTO<TransactionDTO>(transactions));
        }
    }
}
