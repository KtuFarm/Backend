using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Models.ProductBalanceEntity.DTO;
using Newtonsoft.Json;

namespace Backend.Models.OrderEntity.DTO
{
    public class OrderFullDTO : OrderDTO
    {
        [JsonProperty("products")]
        public List<ProductBalanceDTO> Products { get; set; }

        [JsonProperty("total")]
        public decimal Total { get; set; }

        public OrderFullDTO(Order o) : base(o)
        {
            Products = o.OrderProductBalances
                .Select(opb => new ProductBalanceDTO(opb.ProductBalance))
                .ToList();

            Total = (decimal) Math.Round(o.Total, 2);
        }
    }
}
