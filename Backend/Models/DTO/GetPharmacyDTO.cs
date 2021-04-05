using System.Collections.Generic;
using Backend.Models.Database;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class GetPharmacyDTO
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public PharmacyFullDTO Data { get; set; }

        public GetPharmacyDTO(Pharmacy pharmacy, IEnumerable<WorkingHours> workingHours)
        {
            Meta = new Meta();
            Data = new PharmacyFullDTO(pharmacy, workingHours);
        }
    }
}
