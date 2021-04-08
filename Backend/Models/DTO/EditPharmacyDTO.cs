using Newtonsoft.Json;
using System.Collections.Generic;

namespace Backend.Models.DTO
{
    public class EditPharmacyDTO
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
        
        [JsonProperty("workingHours")]
        public List<WorkingHoursDTO> WorkingHours { get; set; }
    }
}
