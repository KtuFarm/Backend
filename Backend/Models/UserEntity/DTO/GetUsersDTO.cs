using System.Collections.Generic;
using Backend.Models.Common;
using Newtonsoft.Json;

namespace Backend.Models.UserEntity.DTO
{
    public class GetUsersDTO
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public IEnumerable<GetUserDTO> Data { get; set; }

        public GetUsersDTO(IEnumerable<GetUserDTO> users)
        {
            Meta = new Meta();
            Data = users;
        }
    }
}
