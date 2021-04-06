using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Backend.Models.DTO
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
