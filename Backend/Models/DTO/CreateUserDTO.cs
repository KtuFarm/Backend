using Newtonsoft.Json;
using System;

namespace Backend.Models.DTO
{
    public class CreateUserDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("registrationDate")]
        public DateTime RegistrationDate { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("employeeState")]
        public int? EmployeeState { get; set; }
    }
}
