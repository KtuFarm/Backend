using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class TransactionProductDTO
    {
        [JsonProperty("ProductBalanceId")]
        public int ProductBalanceId { get; set; }
        
        [JsonProperty("amount")]
        public double Amount { get; set; }
    }
}
