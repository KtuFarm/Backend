using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class ErrorDTO
    {
        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }
    }
}
