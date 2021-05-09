using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Exceptions;
using Backend.Models;
using Backend.Models.Common;
using Backend.Models.DTO;
using Backend.Models.UserEntity;
using Backend.Models.UserEntity.DTO;
using Backend.Services.Jwt;
using Backend.Services.Validators.UserDTOValidator;
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
        private const string ModelName = "user";

        private readonly IJwtService _jwt;
        private readonly IUserDTOValidator _validator;

        public UsersController(
            ApiContext context,
            UserManager<User> userManager,
            IJwtService jwt,
            IUserDTOValidator validator
        ) : base(context, userManager)
        {
            _jwt = jwt;
            _validator = validator;
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

            if (user == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

            return Ok(new GetObjectDTO<UserFullDTO>(user));
        }

        [HttpGet("me")]
        [Authorize(Roles = AllRoles)]
        public async Task<ActionResult<GetObjectDTO<UserFullDTO>>> GetUser()
        {
            var user = await GetCurrentUser();

            if (user == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

            var dto = new UserFullDTO(user);
            return Ok(new GetObjectDTO<UserFullDTO>(dto));
        }

        [HttpPost]
        [Obsolete("use `api/v1/users/signup` instead")]
        public async Task<ActionResult> AddUser([FromBody] CreateUserDTO dataFromBody)
        {
            // ValidateCreateUserDTO(dataFromBody);

            await Context.Users.AddAsync(new User(dataFromBody));
            await Context.SaveChangesAsync();

            return Created();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditUser(int id, [FromBody] EditUserDTO dto)
        {
            var user = await Context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

            try
            {
                _validator.ValidateEditUserDTO(dto);
                user.UpdateFromDTO(dto);

                await Context.SaveChangesAsync();
                return Ok();
            }
            catch (DtoValidationException ex)
            {
                return ApiBadRequest(ex.Message, ex.Parameter);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> Login([FromBody] LoginDTO dto)
        {
            try
            {
                _validator.ValidateLoginDTO(dto);
                var user = await UserManager.FindByEmailAsync(dto.Email);

                if (user == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

                if (!await UserManager.CheckPasswordAsync(user, dto.Password))
                {
                    return ApiBadRequest(ApiErrorSlug.InvalidPassword);
                }

                string token = GenerateToken(user);
                return Ok(new { jwt = token });
            }
            catch (DtoValidationException ex)
            {
                return ApiBadRequest(ex.Message, ex.Parameter);
            }
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup([FromBody] CreateUserDTO dto)
        {
            try
            {
                _validator.ValidateCreateUserDTO(dto);
                await ValidatePassword(dto.Password);

                var user = new User(dto);
                var result = await UserManager.CreateAsync(user, dto.Password);

                return result.Succeeded ? Created() : ApiBadRequest(result.Errors.First().Description);
            }
            catch (DtoValidationException ex)
            {
                return ApiBadRequest(ex.Message, ex.Parameter);
            }
        }

        private async Task ValidatePassword(string password)
        {
            foreach (var validator in UserManager.PasswordValidators)
            {
                var res = await validator.ValidateAsync(UserManager, null, password);
                if (!res.Succeeded)
                {
                    throw new DtoValidationException(res.Errors.First().Description);
                }
            }
        }

        private string GenerateToken(User user)
        {
            return _jwt.GenerateSecurityToken(new JwtUser(user.Email, user.DepartmentId));
        }
    }
}
