using System.Collections.Generic;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class CreateTransactionDTO
    {
        [JsonProperty("pharmacyId")]
        public int PharmacyId { get; set; }
        
        [JsonProperty("registerId")]
        public int RegisterId { get; set; }

        [JsonProperty("products")] 
        public IEnumerable<TransactionProductDTO> Products;
    }
}
