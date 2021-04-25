using System.Collections.Generic;
using System.Linq;
using Backend.Models.Common;
using Newtonsoft.Json;

namespace Backend.Models.ProductBalanceEntity.DTO
{
    public class GetProductBalancesDTO
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public IEnumerable<ProductBalanceDTO> Data { get; set; }

        public GetProductBalancesDTO(IEnumerable<ProductBalance> products)
        {
            Meta = new Meta();
            Data = products.Select(p => new ProductBalanceDTO(p));
        }
    }
}
