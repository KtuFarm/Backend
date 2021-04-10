using Backend.Models.Database;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class MedicamentFullDTO: MedicamentDTO
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        
        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }
        
        [JsonProperty("pharmaceuticalForm")]
        public string PharmaceuticalForm { get; set; }
        
        [JsonProperty("isReimbursed")]
        public bool IsReimbursed { get; set; }
        
        [JsonProperty("reimbursePercentage")]
        public double? ReimbursePercentage { get; set; }

        [JsonProperty("priceWithoutReimburse")]
        public decimal? PriceWithoutReimburse { get; set; }
        
        public MedicamentFullDTO(Medicament medicament) : base(medicament)
        {
            Country = medicament.Country;
            Manufacturer = medicament.Manufacturer?.Name ?? "";
            PharmaceuticalForm = medicament.PharmaceuticalFormId.ToString();
            ReimbursePercentage = medicament.ReimbursePercentage;
            PriceWithoutReimburse = medicament.CalculateFullPrice();
        }
    }
}
