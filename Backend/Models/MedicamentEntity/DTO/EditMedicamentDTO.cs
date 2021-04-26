using Newtonsoft.Json;

namespace Backend.Models.MedicamentEntity.DTO
{
    public class EditMedicamentDTO
    {
        [JsonProperty("isPrescriptionRequired")]
        public bool? IsPrescriptionRequired { get; set; }
        
        [JsonProperty("basePrice")]
        public decimal? BasePrice { get; set; }

        [JsonProperty("surcharge")]
        public double? Surcharge { get; set; }
        
        [JsonProperty("isReimbursed")]
        public bool? IsReimbursed { get; set; }

        [JsonProperty("reimbursePercentage")]
        public double? ReimbursePercentage { get; set; }
    }
}
