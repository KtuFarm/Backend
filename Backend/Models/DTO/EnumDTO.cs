using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class EnumDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
