using Newtonsoft.Json;
using System.Collections.Generic;

namespace Backend.Models.DTO
{
    public class GetPharmaciesDTO
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public IEnumerable<PharmacyDTO> Data { get; set; }

        public GetPharmaciesDTO(IEnumerable<PharmacyDTO> pharmacies)
        {
            Meta = new Meta();
            Data = pharmacies;
        }
    }
}
