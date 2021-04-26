using System.Collections.Generic;
using System.Linq;
using Backend.Models.Common;
using Newtonsoft.Json;

namespace Backend.Models.TransactionEntity.DTO
{
    public class GetTransactionsDTO
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public IEnumerable<TransactionDTO> Data { get; set; }

        public GetTransactionsDTO(IEnumerable<Transaction> transactions)
        {
            Meta = new Meta();
            Data = transactions.Select(t => new TransactionDTO(t));
        }
    }
}
