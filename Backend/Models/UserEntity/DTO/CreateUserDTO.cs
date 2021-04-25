using Newtonsoft.Json;

namespace Backend.Models.UserEntity.DTO
{
    public class CreateUserDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("pharmacyId")]
        public int? PharmacyId { get; set; }
    }
}
