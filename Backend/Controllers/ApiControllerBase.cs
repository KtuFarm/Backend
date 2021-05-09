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

        protected readonly UserManager<User> UserManager;

        protected ApiContext Context { get; }

        private const string ApiHeader = "X-Api-Request";

        public ApiControllerBase(ApiContext context, UserManager<User> userManager)
        {
            Context = context;
            UserManager = userManager;
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

        protected ActionResult InvalidHeaders()
        {
            return ApiBadRequest("Invalid Headers!");
        }
    }
}
