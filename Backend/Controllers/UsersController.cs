using Backend.Models;
using Backend.Models.Database;
using Backend.Models.DTO;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPharmacyDTO>> GetUser(int id)
        {
            var user = await Context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return ApiNotFound("User does not exist!");

            return Ok(new GetUserDTO(user));
        }

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] CreateUserDTO dataFromBody)
        {
            ValidateCreateUserDTO(dataFromBody);

            Context.Users.Add(new User(dataFromBody));
            await Context.SaveChangesAsync();

            return Created();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditUser(int id, [FromBody] EditUserDTO dto)
        {
            var user = await Context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return ApiNotFound("User does not exist!");

            user.UpdateFromDTO(dto);

            if (!string.IsNullOrEmpty(dto.EmployeeState))
            {
                var employeeState = await Context.EmployeeState
                    .FirstOrDefaultAsync(es => es.Name == dto.EmployeeState);
                if (employeeState != null) user.EmployeeStateId = employeeState.Id;
            }

            await Context.SaveChangesAsync();
            return Ok();
        }

        [AssertionMethod]
        private static void ValidateCreateUserDTO(CreateUserDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentException("Name is empty!");
            if (string.IsNullOrEmpty(dto.Surname)) throw new ArgumentException("Surname is empty!");
            if (string.IsNullOrEmpty(dto.Position)) throw new ArgumentException("Position is empty!");
        }
    }
}
