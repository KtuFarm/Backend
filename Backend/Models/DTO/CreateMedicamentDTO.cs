using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class CreateMedicamentDTO : EditMedicamentDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("activeSubstance")]
        public string ActiveSubstance { get; set; }

        [JsonProperty("barCode")]
        public string BarCode { get; set; }

        [JsonProperty("pharmaceuticalFormId")]
        public int? PharmaceuticalFormId { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }

}
