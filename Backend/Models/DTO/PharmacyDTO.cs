using System.Collections.Generic;
using System.Linq;
using Backend.Models.Database;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class PharmacyDTO
    {
        [JsonProperty("pharmacy_no")]
        public int Id { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("registers")]
        public IEnumerable<RegisterDTO> Registers { get; set; }

        public PharmacyDTO(Pharmacy pharmacy)
        {
            Id = pharmacy.Id;
            Address = pharmacy.Address;
            City = pharmacy.City;
            Registers = pharmacy.Registers.Select(r => new RegisterDTO(r));
        }
    }
}
