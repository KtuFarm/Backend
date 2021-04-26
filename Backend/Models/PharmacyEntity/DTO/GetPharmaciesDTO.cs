using System.Collections.Generic;
using Backend.Models.Common;
using Newtonsoft.Json;

namespace Backend.Models.PharmacyEntity.DTO
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
