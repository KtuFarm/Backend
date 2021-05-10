using Backend.Exceptions;
using Backend.Models.DTO;
using Backend.Models.UserEntity.DTO;
using Backend.Services.Validators.UserDTOValidator;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace BackendTests
{
    public class UserValidatorTests
    {
        private IUserDTOValidator _validator;

        private static CreateUserDTO ValidCreateDto =>
            new()
            {
                Email = "testmail@test.com",
                Password = "super_secure_password",
                Name = "Test",
                Surname = "Username",
                Position = "Test user",
                DepartmentId = 0
            };

        private static EditUserDTO ValidEditUserDTO =>
            new()
            {
                Name = "Test",
                Surname = "Username",
                Position = "Test user edited"
            };

        private static LoginDTO ValidLoginDto =>
            new()
            {
                Email = "test.user@test.com",
                Password = "password123"
            };

        [SetUp]
        public void SetUp()
        {
            _validator = new UserDTOValidator();
        }

        [Test]
        public void TestValidCreateDto()
        {
            var dto = ValidCreateDto;

            _validator.ValidateCreateUserDTO(dto);

            Pass();
        }

        [Test]
        public void TestInvalidCreateDto()
        {
            var dto = ValidCreateDto;
            dto.Email = "";

            Throws<DtoValidationException>(() => _validator.ValidateCreateUserDTO(dto));
        }

        [Test]
        public void TestValidEditDto()
        {
            var dto = ValidEditUserDTO;

            _validator.ValidateEditUserDTO(dto);

            Pass();
        }

        [Test]
        public void TestValidLoginDto()
        {
            var dto = ValidLoginDto;

            _validator.ValidateLoginDTO(dto);

            Pass();
        }

        [Test]
        public void TestInvalidLoginDto()
        {
            var dto = ValidLoginDto;
            dto.Email = "";

            Throws<DtoValidationException>(() => _validator.ValidateLoginDTO(dto));
        }
    }
}
