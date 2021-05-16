using Backend.Exceptions;
using Backend.Models;
using Backend.Models.Common;
using Backend.Models.Database;
using Backend.Models.DTO;
using Backend.Models.OrderEntity;
using Backend.Models.ReportEntity;
using Backend.Models.ReportEntity.DTO;
using Backend.Models.TransactionEntity;
using Backend.Models.UserEntity;
using Backend.Services.Validators.ReportDTOValidator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
        [Authorize(Roles = "Admin, Pharmacy")]
        public async Task<ActionResult<GetListDTO<ReportDTO>>> GetReports()
        {
            var reports = await Context.Reports
                .Select(r => new ReportDTO(r))
                .ToListAsync();

            return Ok(new GetListDTO<ReportDTO>(reports));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Pharmacy")]
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

        [HttpPost]
        [Authorize(Roles = "Admin, Pharmacy")]
        public async Task<IActionResult> CreateReport([FromBody] CreateReportDTO dto)
        {
            try
            {
                _validator.ValidateCreateReportDTO(dto);

                var user = await GetCurrentUser();
                if (user.PharmacyId == null) return ApiUnauthorized();
                var pharmacyId = user.PharmacyId ?? -1;

                var orders = Context.Orders.AsEnumerable().Where(o => IsOrderNeeded(o, pharmacyId, dto)).ToList();
                var transactions = Context.Transactions.AsEnumerable().Where(t => IsTransactionNeeded(t, pharmacyId, dto)).ToList();
                var cashTransactions = transactions.Where(t => t.PaymentTypeId == PaymentTypeId.Cash);
                var totalRevenue = CalculateRevenue(transactions);
                var revenueInCash = CalculateRevenue(cashTransactions);
                var totalOrderSum = CalculateTotalOrderSum(orders);
                await Context.Reports.AddAsync(new Report(dto, pharmacyId, orders.Count, transactions.Count, totalRevenue, revenueInCash, totalOrderSum));

                await Context.SaveChangesAsync();
                return Created();
            }
            catch(DtoValidationException ex)
            {
                return ApiBadRequest(ex.Message, ex.Parameter);
            }
        }

        private bool IsOrderNeeded(Order order, int pharmacyId, CreateReportDTO dto)
        {
            return order.PharmacyId == pharmacyId
                && order.CreationDate >= dto.DateFrom
                && order.CreationDate <= dto.DateTo
                && order.OrderStateId != OrderStateId.Canceled;
        }

        private bool IsTransactionNeeded(Transaction transaction, int pharmacyId, CreateReportDTO dto)
        {
            return transaction.PharmacyId == pharmacyId
                && transaction.CreatedAt >= dto.DateFrom
                && transaction.CreatedAt <= dto.DateTo;
        }

        private decimal CalculateRevenue(IEnumerable<Transaction> transactions)
        {
            return transactions.Sum(t => t.PriceWithoutVat);
        }

        private decimal CalculateTotalOrderSum(IEnumerable<Order> orders)
        {
            return orders.Sum(o => o.TotalSum);
        }
    }
}
