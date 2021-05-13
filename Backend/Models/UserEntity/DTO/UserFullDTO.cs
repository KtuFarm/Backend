using System;
using Newtonsoft.Json;

namespace Backend.Models.UserEntity.DTO
{
    public class UserFullDTO : UserDTO
    {
        [JsonProperty("registrationDate")]
        public DateTime RegistrationDate { get; set; }

        [JsonProperty("dismissalDate")]
        public DateTime? DismissalDate { get; set; }

        [JsonProperty("employeeState")]
        public string EmployeeState { get; set; }

        [JsonProperty("pharmacyId")]
        public int? PharmacyId { get; set; }

        [JsonProperty("warehouseId")]
        public int? WarehouseId { get; set; }

        public UserFullDTO(User user) : base(user)
        {
            RegistrationDate = user.RegistrationDate;
            DismissalDate = user.DismissalDate;
            EmployeeState = user.EmployeeStateId.ToString();
            PharmacyId = user.PharmacyId;
            WarehouseId = user.WarehouseId;
        }
    }
}
