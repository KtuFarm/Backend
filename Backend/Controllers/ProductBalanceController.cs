using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Models.Database;
using Backend.Models.DTO;
using Backend.Models.ProductBalanceEntity;
using Backend.Models.ProductBalanceEntity.DTO;
using Backend.Models.UserEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/v1/Products")]
    [ApiController]
    public class ProductBalanceController : ApiControllerBase
    {
        public ProductBalanceController(ApiContext context, UserManager<User> userManager) :
            base(context, userManager) { }

        [HttpGet]
        [Authorize(Roles = "Pharmacy, Warehouse")]
        public async Task<ActionResult<GetListDTO<ProductBalanceDTO>>> GetProductBalances()
        {
            var user = await GetCurrentUser();

            var products = await GetProductsQuery(user)
                .Include(pb => pb.Medicament)
                .Select(pb => new ProductBalanceDTO(pb))
                .ToListAsync();

            return Ok(new GetListDTO<ProductBalanceDTO>(products));
        }

        private IQueryable<ProductBalance> GetProductsQuery(User user)
        {
            return user.DepartmentId switch
            {
                DepartmentId.Pharmacy => Context.ProductBalances.Where(o => o.PharmacyId == user.PharmacyId),
                DepartmentId.Warehouse => Context.ProductBalances.Where(o => o.WarehouseId == user.WarehouseId),
                _ => null
            };
        }
    }
}
