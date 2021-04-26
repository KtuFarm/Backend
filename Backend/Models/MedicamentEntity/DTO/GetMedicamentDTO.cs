using Backend.Models.Common;
using Newtonsoft.Json;

namespace Backend.Models.MedicamentEntity.DTO
{
    public class GetMedicamentDTO
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public MedicamentFullDTO Data { get; set; }

        public GetMedicamentDTO(Medicament medicament)
        {
            Meta = new Meta();
            Data = new MedicamentFullDTO(medicament);
        }
    }
}
