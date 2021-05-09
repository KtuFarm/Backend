using System;
using Backend.Exceptions;
using Backend.Models.Common;
using Backend.Models.Database;
using Backend.Models.DTO;
using Backend.Models.UserEntity.DTO;
using JetBrains.Annotations;

namespace Backend.Services.Validators.UserDTOValidator
{
    public class UserDTOValidator : DTOValidator, IUserDTOValidator
    {
        public void ValidateCreateUserDTO(CreateUserDTO dto)
        {
            ValidateString(dto.Email, "email");
            ValidateString(dto.Password, "password");
            ValidateString(dto.Name, "name");
            ValidateString(dto.Surname, "surname");
            ValidateString(dto.Position, "position");
        }

        public void ValidateEditUserDTO(EditUserDTO dto)
        {
            if (dto.Name != null) ValidateString(dto.Name, "name");
            if (dto.Surname != null) ValidateString(dto.Surname, "surname");
            if (dto.Position != null) ValidateString(dto.Position, "position");
            if (dto.EmployeeStateId != null) ValidateEmployeeStateId(dto.EmployeeStateId);
        }

        public void ValidateLoginDTO(LoginDTO dto)
        {
            ValidateString(dto.Email, "email");
            ValidateString(dto.Password, "password");
        }

        [AssertionMethod]
        private static void ValidateEmployeeStateId(int? id)
        {
            if (!Enum.IsDefined(typeof(EmployeeStateId), id ?? -1))
            {
                throw new DtoValidationException(ApiErrorSlug.InvalidEmployeeState);
            }
        }
    }
}
