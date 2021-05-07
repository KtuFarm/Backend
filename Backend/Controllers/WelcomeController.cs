using Backend.Models;
using Backend.Models.DTO;
using Backend.Models.UserEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Produces(ApiContentType)]
    public class WelcomeController : ApiControllerBase
    {
        public WelcomeController(ApiContext context, UserManager<User> userManager) : base(context, userManager) { }

        [HttpGet]
        public ActionResult<MessageDTO> GetQuizMessage()
        {
            var message = new MessageDTO("Hello World!");
            return Ok(message);
        }
    }
}
