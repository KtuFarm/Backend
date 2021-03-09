using System.Linq;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        private const string ApiHeader = "X-Api-Request";
        protected const string ApiContentType = "application/json";

        protected bool IsValidApiRequest()
        {
            Request.Headers.TryGetValue(ApiHeader, out var headers);
            if (headers.Count != 1 || string.IsNullOrWhiteSpace(headers.FirstOrDefault()))
            {
                return false;
            }

            return headers == "true";
        }

        protected BadRequestObjectResult ApiBadRequest(string message)
        {
            var error = new ErrorDTO
            {
                Type = 400,
                Title = message
            };
            return BadRequest(error);
        }
    }
}
