using Backend.Models.Database;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class PharmacyDTO
    {
        [JsonProperty("pharmacyNo")]
        public int Id { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
        
        public PharmacyDTO(Pharmacy pharmacy)
        {
            Id = pharmacy.Id;
            Address = pharmacy.Address;
            City = pharmacy.City;
        }
    }
}
