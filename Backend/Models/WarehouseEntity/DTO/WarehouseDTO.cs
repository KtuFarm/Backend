using Newtonsoft.Json;

namespace Backend.Models.WarehouseEntity.DTO
{
    public class WarehouseDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        public WarehouseDTO(Warehouse warehouse)
        {
            Id = warehouse.Id;
            Address = warehouse.Address;
            City = warehouse.City;
        }
    }
}
