using Backend.Models;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected const string ApiContentType = "application/json";

        protected ApiContext Context { get; }

        public ApiControllerBase(ApiContext context)
        {
            Context = context;
        }

        protected ActionResult Created()
        {
            return StatusCode(201);
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
