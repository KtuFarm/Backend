using Backend.Models.UserEntity;

namespace Backend.Services.Jwt
{
    public interface IJwtService
    {
        public string GenerateSecurityToken(JwtUser jwtUser);
    }
}
