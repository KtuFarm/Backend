using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Database;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class GetMedicamentDTO
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public MedicamentDTO Data { get; set; }

        public GetMedicamentDTO(Medicament medicament)
        {
            Meta = new Meta();
            Data = new MedicamentDTO(medicament);
        }
    }
}
