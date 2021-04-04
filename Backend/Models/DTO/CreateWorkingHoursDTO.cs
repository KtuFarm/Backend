using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class CreateWorkingHoursDTO
    {
        [JsonProperty("openTime")]
        public string OpenTime { get; set; }

        [JsonProperty("closeTime")]
        public string CloseTime { get; set; }
        
        [JsonProperty("dayOfWeek")]
        public int DayOfWeek { get; set; }
    }
}
