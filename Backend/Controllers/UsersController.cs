using Backend.Models;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        public UsersController(ApiContext context) : base(context) {}

        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetUsers()
        {
            var users = await Context.Users
                .Select(u => new GetUserDTO(u))
                .ToListAsync();

            return Ok(new GetUsersDTO(users));
        }
    }
}
