using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class LoginDTO
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
