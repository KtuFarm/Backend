using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Models.Database;
using Backend.Models.DTO;
using Backend.Models.UserEntity;
using Backend.Models.UserEntity.DTO;
using Backend.Services.Jwt;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        private readonly IJwtService _jwt;

        public UsersController(ApiContext context, IJwtService jwt, UserManager<User> userManager) : base(context,
            userManager)
        {
            _jwt = jwt;
        }

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
        [Obsolete]
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

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            /*if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }*/

            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
                return ApiBadRequest("User does not exist.");

            if (await UserManager.CheckPasswordAsync(user, model.Password))
            {
                return Ok(new
                {
                    token = _jwt.GenerateSecurityToken(new JwtUser()
                    {
                        Email = user.Email,
                        RoleId = user.DepartmentId
                    })
                });
            }

            return ApiBadRequest("Bad password");
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<ActionResult<GetObjectDTO<string>>> Signup([FromBody] CreateUserDTO dto)
        {
            // TODO: Validation
            ValidateCreateUserDTO(dto);

            /*if (!IsValidApiRequest())
            {
                return ApiBadRequest("Invalid Headers!");
            }*/

            /*foreach (var validator in UserManager.PasswordValidators)
            {
                var res = await validator.ValidateAsync(UserManager, null, dto.Password);
                if (!res.Succeeded)
                    return ApiBadRequest(res.Errors.First().Description);
            }*/

            var user = new User(dto);
            var result = await UserManager.CreateAsync(user, dto.Password);
            
            if (!result.Succeeded)
                return ApiBadRequest(result.Errors.First().Description);

            string token = _jwt.GenerateSecurityToken(new JwtUser
            {
                Email = dto.Email,
                RoleId = (DepartmentId) dto.DepartmentId
            });

            return Created(new GetObjectDTO<string>(token));
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
