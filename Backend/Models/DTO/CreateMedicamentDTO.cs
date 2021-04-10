using Backend.Models.Database;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class CreateMedicamentDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("activeSubstance")]
        public string ActiveSubstance { get; set; }

        [JsonProperty("barCode")]
        public string BarCode { get; set; }

        [JsonProperty("pharmaceuticalFormId")]
        public int PharmaceuticalFormId { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
        
        [JsonProperty("isPrescriptionRequired")]
        public bool IsPrescriptionRequired { get; set; }
        
        [JsonProperty("basePrice")]
        public decimal BasePrice { get; set; }

        [JsonProperty("surcharge")]
        public double Surcharge { get; set; }
        
        [JsonProperty("isReimbursed")]
        public bool IsReimbursed { get; set; }
        
        [JsonProperty("reimbursePercentage")]
        public double? ReimbursePercentage { get; set; }
    }
}
