using System.Collections.Generic;
using Backend.Models.WorkingHoursEntity.DTO;
using Newtonsoft.Json;

namespace Backend.Models.PharmacyEntity.DTO
{
    public class CreatePharmacyDTO
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
        
        [JsonProperty("registersCount")]
        public int RegistersCount { get; set; }

        [JsonProperty("workingHours")]
        public List<WorkingHoursDTO> WorkingHours { get; set; }
    }
}
