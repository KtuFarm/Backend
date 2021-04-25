using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Models.DTO;
using Backend.Models.UserEntity;
using Backend.Models.UserEntity.DTO;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        public UsersController(ApiContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<GetListDTO<UserDTO>>> GetUsers()
        {
            var users = await Context.Users
                .Select(u => new UserDTO(u))
                .ToListAsync();

            return Ok(new GetListDTO<UserDTO>(users));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetObjectDTO<UserFullDTO>>> GetUser(int id)
        {
            var user = await Context.Users
                .Where(u => u.Id == id)
                .Select(u => new UserFullDTO(u))
                .FirstOrDefaultAsync();

            if (user == null) return ApiNotFound("User does not exist!");

            return Ok(new GetObjectDTO<UserFullDTO>(user));
        }

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] CreateUserDTO dataFromBody)
        {
            ValidateCreateUserDTO(dataFromBody);

            await Context.Users.AddAsync(new User(dataFromBody));
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
