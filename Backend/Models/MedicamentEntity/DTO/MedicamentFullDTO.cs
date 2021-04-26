using Newtonsoft.Json;

namespace Backend.Models.MedicamentEntity.DTO
{
    public class MedicamentFullDTO : MedicamentDTO
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
        public double ReimbursePercentage { get; set; }

        [JsonProperty("basePrice")]
        public decimal BasePrice { get; set; }

        [JsonProperty("surcharge")]
        public double Surcharge { get; set; }

        public MedicamentFullDTO(Medicament medicament) : base(medicament)
        {
            Country = medicament.Country;
            Manufacturer = medicament.Manufacturer?.Name ?? "";
            PharmaceuticalForm = medicament.PharmaceuticalFormId.ToString();
            ReimbursePercentage = medicament.ReimbursePercentage;
            BasePrice = medicament.BasePrice;
            Surcharge = medicament.Surcharge;
        }
    }
}
