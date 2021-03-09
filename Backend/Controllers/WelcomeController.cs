using API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Produces(ApiContentType)]
    public class WelcomeController : ApiControllerBase
    {
        [HttpGet]
        public ActionResult<MessageDTO> GetQuizMessage()
        {
            if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }

            var message = new MessageDTO("Hello World!");
            return Ok(message);
        }
    }
}
