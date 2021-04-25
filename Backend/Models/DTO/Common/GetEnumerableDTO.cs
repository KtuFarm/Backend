using System.Collections.Generic;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class GetEnumerableDTO
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public IEnumerable<EnumDTO> Data { get; set; }

        public GetEnumerableDTO(IEnumerable<EnumDTO> data)
        {
            Meta = new Meta();
            Data = data;
        }
    }
}
