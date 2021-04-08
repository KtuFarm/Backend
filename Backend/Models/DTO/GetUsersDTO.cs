﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Backend.Models.DTO
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