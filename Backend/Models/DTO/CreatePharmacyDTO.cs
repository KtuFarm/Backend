using System.Collections.Generic;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class CreatePharmacyDTO
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
        
        [JsonProperty("workingHours")]
        public ICollection<CreateWorkingHoursDTO> WorkingHours { get; set; }
    }
}
