using System.Collections.Generic;
using Backend.Models.WorkingHoursEntity.DTO;
using Newtonsoft.Json;

namespace Backend.Models.PharmacyEntity.DTO
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
