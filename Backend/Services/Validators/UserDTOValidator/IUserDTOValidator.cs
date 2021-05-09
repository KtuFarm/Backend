using Backend.Models.DTO;
using Backend.Models.UserEntity.DTO;
using JetBrains.Annotations;

namespace Backend.Services.Validators.UserDTOValidator
{
    public interface IUserDTOValidator
    {
        [AssertionMethod]
        public void ValidateCreateUserDTO(CreateUserDTO dto);

        [AssertionMethod]
        public void ValidateEditUserDTO(EditUserDTO dto);

        [AssertionMethod]
        public void ValidateLoginDTO(LoginDTO dto);
    }
}
