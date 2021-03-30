﻿using System;
using Backend.Models.Database;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class RegisterDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("cash")]
        public decimal Cash { get; set; }

        public RegisterDTO(Register register)
        {
            Id = register.Id;
            Cash = Math.Round(register.Cash, 2);
        }
    }
}
