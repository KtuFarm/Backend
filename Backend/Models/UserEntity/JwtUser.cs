using Backend.Models.Database;

namespace Backend.Models.UserEntity
{
    public class JwtUser
    {
        public string Email { get; set; }
        public DepartmentId RoleId { get; set; }
    }
}
