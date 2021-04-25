using System;
using Newtonsoft.Json;

namespace Backend.Models.UserEntity.DTO
{
    public class UserFullDTO: UserDTO
    {
        [JsonProperty("registrationDate")]
        public DateTime RegistrationDate { get; set; }

        [JsonProperty("dismissalDate")]
        public DateTime? DismissalDate { get; set; }

        [JsonProperty("employeeState")]
        public string EmployeeState { get; set; }

        public UserFullDTO(User user) : base(user)
        {
            RegistrationDate = user.RegistrationDate;
            DismissalDate = user.DismissalDate;
            EmployeeState = user.EmployeeStateId.ToString();
        }
    }
}
