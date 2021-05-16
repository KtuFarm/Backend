using Backend.Models;
using Backend.Models.Common;
using Backend.Models.DTO;
using Backend.Models.ReportEntity.DTO;
using Backend.Models.UserEntity;
using Backend.Services.Validators.ReportDTOValidator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReportsController : ApiControllerBase
    {
        private const string ModelName = "report";
        private readonly IReportDTOValidator _validator;

        public ReportsController(ApiContext context, IReportDTOValidator validator, UserManager<User> userManager)
            : base(context, userManager)
        {
            _validator = validator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<GetListDTO<ReportDTO>>> GetReports()
        {
            var reports = await Context.Reports
                .Select(r => new ReportDTO(r))
                .ToListAsync();

            return Ok(new GetListDTO<ReportDTO>(reports));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GetObjectDTO<ReportFullDTO>>> GetReport(int id)
        {
            var report = await Context.Reports
                                .Where(r => r.Id == id)
                                .Include(r => r.User)
                                .Include(r => r.Pharmacy)
                                .Select(r => new ReportFullDTO(r))
                                .FirstOrDefaultAsync();

            if (report == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

            return Ok(new GetObjectDTO<ReportFullDTO>(report));
        }
    }
}
