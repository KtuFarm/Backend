using System;
using Backend.Models;
using Backend.Models.Common;
using Backend.Models.DTO;
using Backend.Models.WarehouseEntity.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.ProductBalanceEntity.DTO;
using Backend.Models.UserEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WarehousesController : ApiControllerBase
    {
        private const string ModelName = "warehouse";

        public WarehousesController(ApiContext context, UserManager<User> userManager) : base(context, userManager) { }

        [HttpGet]
        public async Task<ActionResult<GetListDTO<WarehouseDTO>>> GetWarehouses()
        {
            var warehouses = await Context.Warehouses
                .Select(w => new WarehouseDTO(w))
                .ToListAsync();

            return Ok(new GetListDTO<WarehouseDTO>(warehouses));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetObjectDTO<WarehouseDTO>>> GetWarehouse(int id)
        {
            var warehouse = await Context.Warehouses
                .Where(w => w.Id == id)
                .Select(w => new WarehouseDTO(w))
                .FirstOrDefaultAsync();

            if (warehouse == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

            return Ok(new GetObjectDTO<WarehouseDTO>(warehouse));
        }

        [HttpGet("{id}/products")]
        [Authorize(Roles = "Pharmacy, Warehouse")]
        public async Task<ActionResult<GetListDTO<ProductBalanceDTO>>> GetProductBalances(int id)
        {
            var products = await Context.ProductBalances
                .Include(pb => pb.Medicament)
                .Where(pb => pb.WarehouseId == id)
                .Select(p => new ProductBalanceDTO(p))
                .ToListAsync();

            return Ok(new GetListDTO<ProductBalanceDTO>(products));
        }
    }
}
