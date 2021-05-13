using System;
using System.Collections.Generic;
using Backend.Models.DTO;
using Newtonsoft.Json;

namespace Backend.Models.OrderEntity.DTO
{
    public class CreateOrderDTO
    {
        [JsonProperty("warehouseId")]
        public int WarehouseId { get; set; }

        [JsonProperty("pharmacyId")]
        public int PharmacyId { get; set; }

        [JsonProperty("deliveryDate")]
        public DateTime DeliveryDate { get; set; }

        [JsonProperty("products")]
        public List<TransactionProductDTO> Products { get; set; }
    }
}
