using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Models.DTO;
using Backend.Models.UserEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected const string ApiContentType = "application/json";
        protected const string AllRoles = "Pharmacy, Warehouse, Admin, Transportation, Manufacturer";

        protected readonly UserManager<User> UserManager;

        protected ApiContext Context { get; }

        public ApiControllerBase(ApiContext context, UserManager<User> userManager)
        {
            Context = context;
            UserManager = userManager;
        }

        protected async Task<User> GetCurrentUser()
        {
            string email = User.FindFirst(ClaimTypes.Email)?.Value;

            return await UserManager.FindByEmailAsync(email);
        }

        protected ActionResult Created()
        {
            return StatusCode(201);
        }

        protected ActionResult Created(object data)
        {
            return StatusCode(201, data);
        }

        protected BadRequestObjectResult ApiBadRequest(string message, string details = null)
        {
            var error = new ErrorDTO
            {
                Type = 400,
                Title = message,
                Details = details
            };
            return BadRequest(error);
        }

        protected NotFoundObjectResult ApiNotFound(string message, string details = null)
        {
            var error = new ErrorDTO
            {
                Type = 404,
                Title = message,
                Details = details
            };
            return NotFound(error);
        }
    }
}
