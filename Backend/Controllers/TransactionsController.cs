using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Models.Common;
using Backend.Models.Database;
using Backend.Models.TransactionEntity;
using Backend.Models.TransactionEntity.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransactionsController : ApiControllerBase
    {
        public TransactionsController(ApiContext context) : base(context) { }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDTO dto)
        {
            var pharmacy = await Context
                .Pharmacies
                .Include(p => p.Registers)
                .Include(p => p.Products)
                .ThenInclude(pb => pb.Medicament)
                .FirstOrDefaultAsync(p => p.Id == dto.PharmacyId);
            if (pharmacy == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, "pharmacy");

            var register = pharmacy.Registers.FirstOrDefault(r => r.Id == dto.RegisterId);
            if (register == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, "register");

            var transaction = new Transaction
            {
                PharmacyId = pharmacy.Id,
                RegisterId = register.Id,
                PaymentTypeId = (PaymentTypeId) dto.PaymentTypeId
            };
            
            foreach (var product in dto.Products)
            {
                var productInPharmacy = pharmacy.Products.FirstOrDefault(pb => pb.Id == product.ProductBalanceId);
                if (productInPharmacy == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, "productBalance");
                if (productInPharmacy.Amount < product.Amount) 
                    return ApiBadRequest($"Invalid {productInPharmacy.Medicament.Name} amount!");

                productInPharmacy.Amount -= product.Amount;
                transaction.AddProduct(productInPharmacy, product.Amount);
            }

            Context.Add(transaction);
            await Context.SaveChangesAsync();
           
            return Created();
        }
    }
}
