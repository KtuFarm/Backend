using Backend.Models.Database;
using Backend.Models.MedicamentEntity;
using Backend.Models.PharmacyEntity;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class RequiredMedicamentAmountDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("pharmacy")]
        public Pharmacy Pharmacy { get; set; }

        [JsonProperty("medicament")]
        public Medicament Medicament { get; set; }

        public RequiredMedicamentAmountDTO(RequiredMedicamentAmount requiredAmount)
        {
            Id = requiredAmount.Id;
            Amount = requiredAmount.Amount;
            Pharmacy = requiredAmount.Pharmacy;
            Medicament = requiredAmount.Medicament;
        }
    }
}
