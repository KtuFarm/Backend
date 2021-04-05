using Backend.Models.Database;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class WorkingHoursDTO
    {
        [JsonProperty("openTime")]
        public string OpenTime { get; set; }

        [JsonProperty("closeTime")]
        public string CloseTime { get; set; }
        
        [JsonProperty("dayOfWeek")]
        public int DayOfWeek { get; set; }

        public WorkingHoursDTO() { }

        public WorkingHoursDTO(WorkingHours hours)
        {
            OpenTime = $"{hours.OpenTime.Hours:00}:{hours.OpenTime.Minutes:00}";
            CloseTime = $"{hours.CloseTime.Hours:00}:{hours.CloseTime.Minutes:00}";
            DayOfWeek = (int) hours.DayOfWeekId;
        }
    }
}
