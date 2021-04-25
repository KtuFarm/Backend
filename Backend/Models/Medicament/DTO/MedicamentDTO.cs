using Backend.Models.Database;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class MedicamentDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("activeSubstance")]
        public string ActiveSubstance { get; set; }

        [JsonProperty("barCode")]
        public string BarCode { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
        
        [JsonProperty("isPrescriptionRequired")]
        public bool IsPrescriptionRequired { get; set; }

        public MedicamentDTO(Medicament medicament)
        {
            Id = medicament.Id;
            Name = medicament.Name;
            ActiveSubstance = medicament.ActiveSubstance;
            BarCode = medicament.BarCode;
            IsPrescriptionRequired = medicament.IsPrescriptionRequired;
            Price = medicament.CalculatePriceReimbursed();
        }
    }
}
