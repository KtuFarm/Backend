using Newtonsoft.Json;

namespace Backend.Models.OrderEntity.DTO
{
    public class OrderDTO
    {
        [JsonProperty("orderId")]
        public int OrderId { get; set; }

        [JsonProperty("addressFrom")]
        public string AddressFrom { get; set; }

        [JsonProperty("addressTo")]
        public string AddressTo { get; set; }

        [JsonProperty("expectedDelivery")]
        public string DeliveryDate { get; set; }

        public OrderDTO(Order o)
        {
            OrderId = o.Id;
            AddressFrom = o.AddressFrom;
            AddressTo = o.AddressTo;
            DeliveryDate = o.DeliveryDate.ToString("O");
        }
    }
}
