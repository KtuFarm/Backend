using Newtonsoft.Json;

namespace Backend.Models.UserEntity.DTO
{
    public class CreateUserDTO
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("departmentId")]
        public int DepartmentId { get; set; }

        [JsonProperty("pharmacyId")]
        public int? PharmacyId { get; set; }

        [JsonProperty("warehouseId")]
        public int? WarehouseId { get; set; }
    }
}
