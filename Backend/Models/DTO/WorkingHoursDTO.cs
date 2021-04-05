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
            OpenTime = $"{hours.OpenTime.Hours}:{hours.OpenTime.Minutes}";
            CloseTime = $"{hours.CloseTime.Hours}:{hours.CloseTime.Minutes}";
            DayOfWeek = (int) hours.DayOfWeekId;
        }
    }
}
