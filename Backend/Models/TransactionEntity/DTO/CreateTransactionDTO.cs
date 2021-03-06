using System.Collections.Generic;
using Backend.Models.DTO;
using Newtonsoft.Json;

namespace Backend.Models.TransactionEntity.DTO
{
    public class CreateTransactionDTO
    {
        [JsonProperty("registerId")]
        public int RegisterId { get; set; }

        [JsonProperty("paymentTypeId")]
        public int PaymentTypeId { get; set; }

        [JsonProperty("products")]
        public IEnumerable<TransactionProductDTO> Products;
    }
}
