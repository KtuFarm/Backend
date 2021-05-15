using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Exceptions;
using Backend.Models;
using Backend.Models.Common;
using Backend.Models.Database;
using Backend.Models.DTO;
using Backend.Models.OrderEntity.DTO;
using Backend.Models.PharmacyEntity;
using Backend.Models.ProductBalanceEntity;
using Backend.Models.RegisterEntity;
using Backend.Models.TransactionEntity;
using Backend.Models.TransactionEntity.DTO;
using Backend.Models.UserEntity;
using Backend.Services.OrdersManager;
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
        private readonly IOrdersManager _ordersManager;

        public TransactionsController(ApiContext context, UserManager<User> userManager, IOrdersManager ordersManager) :
            base(context, userManager)
        {
            _ordersManager = ordersManager;
        }

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
                    await TryAddProduct(pharmacy, product, transaction);
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

        private async Task TryAddProduct(Pharmacy pharmacy, TransactionProductDTO dto, Transaction transaction)
        {
            var productInPharmacy = pharmacy.Products.FirstOrDefault(pb => pb.Id == dto.ProductBalanceId);

            if (productInPharmacy == null) throw new ResourceNotFoundException("productBalance");
            if (productInPharmacy.Amount < dto.Amount)
            {
                throw new ArgumentException("amount");
            }

            productInPharmacy.Amount -= dto.Amount;

            var requiredAmount = pharmacy.RequiredMedicamentAmounts
                .FirstOrDefault(rma => rma.MedicamentId == productInPharmacy.MedicamentId);

            if (IsRestockRequired(requiredAmount, productInPharmacy))
            {
                await Restock(pharmacy, requiredAmount, productInPharmacy);
            }

            transaction.AddProduct(productInPharmacy, dto.Amount);
        }

        private async Task Restock(Pharmacy pharmacy, RequiredMedicamentAmount requiredAmount,
            ProductBalance productInPharmacy)
        {
            var orderDto = await CreateOrderDTO(requiredAmount, productInPharmacy);
            try
            {
                await _ordersManager.TryCreateOrder(orderDto, pharmacy.Id);
            }
            catch (DuplicateObjectException ex)
            {
                var order = await Context.Orders
                    .Where(o => o.Id == ex.Id)
                    .Include(o => o.OrderProductBalances)
                    .ThenInclude(opb => opb.ProductBalance)
                    .FirstOrDefaultAsync();

                order.UpdateFromDTO(orderDto, await _ordersManager.CreateProductBalanceList(orderDto.Products));
                await Context.SaveChangesAsync();
            }
        }

        private async Task<CreateOrderDTO> CreateOrderDTO(RequiredMedicamentAmount requiredAmount,
            ProductBalance productInPharmacy)
        {
            var warehouse = await Context.Warehouses
                .Where(w => w.Id == 1)
                .Include(w => w.Products)
                .FirstOrDefaultAsync();

            var productsToOrder = new TransactionProductDTO
            {
                Amount = requiredAmount.Amount - productInPharmacy.Amount,
                ProductBalanceId = warehouse.GetProductId(productInPharmacy.MedicamentId)
            };

            var orderDto = new CreateOrderDTO
            {
                WarehouseId = warehouse.Id,
                Products = new List<TransactionProductDTO> { productsToOrder }
            };

            return orderDto;
        }

        private static bool IsRestockRequired(RequiredMedicamentAmount requiredAmount, ProductBalance productInPharmacy)
        {
            return requiredAmount != null && requiredAmount.Amount > productInPharmacy.Amount;
        }

        private async Task<Pharmacy> RetrievePharmacy(int? pharmacyId)
        {
            var pharmacy = await Context
                .Pharmacies
                .Include(p => p.Registers)
                .Include(p => p.Products)
                .ThenInclude(pb => pb.Medicament)
                .Include(p => p.RequiredMedicamentAmounts)
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
