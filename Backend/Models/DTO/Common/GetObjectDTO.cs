using Backend.Models.Common;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class GetObjectDTO<T>
    {
        public GetObjectDTO(T data)
        {
            Meta = new Meta();
            Data = data;
        }

        [JsonProperty("meta")] 
        public Meta Meta { get; set; }

        [JsonProperty("data")] 
        public T Data { get; set; }
    }
}
