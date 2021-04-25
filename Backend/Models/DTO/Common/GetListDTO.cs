using System.Collections.Generic;
using Backend.Models.Common;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class GetListDTO<T>
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public IEnumerable<T> Data { get; set; }

        public GetListDTO(IEnumerable<T> data)
        {
            Meta = new Meta();
            Data = data;
        }
    }
}
