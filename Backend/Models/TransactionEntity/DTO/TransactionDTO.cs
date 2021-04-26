using Newtonsoft.Json;

namespace Backend.Models.TransactionEntity.DTO
{
    public class TransactionDTO
    {
        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("totalPrice")]
        public decimal TotalPrice { get; set; }
        
        public TransactionDTO(Transaction t)
        {
            CreatedAt = t.CreatedAt.ToString("O");
            TotalPrice = t.TotalPrice;
        }
    }
}
