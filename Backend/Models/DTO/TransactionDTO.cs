using Backend.Models.Database;
using Newtonsoft.Json;

namespace Backend.Models.DTO
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
