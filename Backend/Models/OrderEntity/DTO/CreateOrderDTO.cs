using System.Collections.Generic;
using Backend.Models.DTO;
using Newtonsoft.Json;

namespace Backend.Models.OrderEntity.DTO
{
    public class CreateOrderDTO
    {
        [JsonProperty("warehouseId")]
        public int WarehouseId { get; set; }

        [JsonProperty("products")]
        public List<TransactionProductDTO> Products { get; set; }
    }
}
