using System;
using Newtonsoft.Json;

namespace Backend.Models.UserEntity.DTO
{
    public class EditUserDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("pharmacyId")]
        public int? PharmacyId { get; set; }

        [JsonProperty("dismissalDate")]
        public DateTime? DismissalDate { get; set; } = null;

        [JsonProperty("employeeStateId")]
        public int? EmployeeStateId { get; set; }
    }
}
