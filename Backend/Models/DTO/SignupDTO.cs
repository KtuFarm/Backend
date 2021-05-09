using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class SignupDTO
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("roleId")]
        public int RoleId { get; set; }
    }
}
