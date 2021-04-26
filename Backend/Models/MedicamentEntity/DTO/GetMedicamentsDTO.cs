using System.Collections.Generic;
using Backend.Models.Common;
using Newtonsoft.Json;

namespace Backend.Models.MedicamentEntity.DTO
{
    public class GetMedicamentsDTO
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public IEnumerable<MedicamentDTO> Data { get; set; }

        public GetMedicamentsDTO(IEnumerable<MedicamentDTO> medicaments)
        {
            Meta = new Meta();
            Data = medicaments;
        }
    }
}
