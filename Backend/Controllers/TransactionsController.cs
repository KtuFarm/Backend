using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Exceptions;
using Backend.Models;
using Backend.Models.Common;
using Backend.Models.Database;
using Backend.Models.DTO;
using Backend.Models.PharmacyEntity;
using Backend.Models.RegisterEntity;
using Backend.Models.TransactionEntity;
using Backend.Models.TransactionEntity.DTO;
using Backend.Models.UserEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransactionsController : ApiControllerBase
    {
        public TransactionsController(ApiContext context, UserManager<User> userManager) :
            base(context, userManager) { }

        [HttpPost]
        [Authorize(Roles = "Pharmacy")]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDTO dto)
        {
            try
            {
                var user = await GetCurrentUser();

                var pharmacy = await RetrievePharmacy(user.PharmacyId);
                var register = RetrieveRegister(pharmacy, dto.RegisterId);

                var transaction = new Transaction(pharmacy.Id, dto);

                foreach (var product in dto.Products)
                {
                    TryAddProduct(pharmacy, product, transaction);
                }

                if (transaction.PaymentTypeId == PaymentTypeId.Cash)
                {
                    register.Cash += transaction.TotalPrice;
                }

                Context.Add(transaction);
                await Context.SaveChangesAsync();

                return Created();
            }
            catch (ResourceNotFoundException ex)
            {
                return ApiNotFound(ApiErrorSlug.ResourceNotFound, ex.Message);
            }
            catch (ArgumentException ex)
            {
                return ApiBadRequest(ApiErrorSlug.InsufficientBalance, ex.Message);
            }
        }

        private static void TryAddProduct(Pharmacy pharmacy, TransactionProductDTO dto, Transaction transaction)
        {
            var productInPharmacy = pharmacy.Products.FirstOrDefault(pb => pb.Id == dto.ProductBalanceId);

            if (productInPharmacy == null) throw new ResourceNotFoundException("productBalance");
            if (productInPharmacy.Amount < dto.Amount)
            {
                throw new ArgumentException("amount");
            }

            productInPharmacy.Amount -= dto.Amount;
            // TODO: Check amount and append to the order of required

            transaction.AddProduct(productInPharmacy, dto.Amount);
        }

        private async Task<Pharmacy> RetrievePharmacy(int? pharmacyId)
        {
            var pharmacy = await Context
                .Pharmacies
                .Include(p => p.Registers)
                .Include(p => p.Products)
                .ThenInclude(pb => pb.Medicament)
                .FirstOrDefaultAsync(p => p.Id == pharmacyId);

            if (pharmacy == null) throw new ResourceNotFoundException("pharmacy");

            return pharmacy;
        }

        private static Register RetrieveRegister(Pharmacy pharmacy, int registerId)
        {
            var register = pharmacy.Registers.FirstOrDefault(r => r.Id == registerId);

            if (register == null) throw new ResourceNotFoundException("register");

            return register;
        }
    }
}
