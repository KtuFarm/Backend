using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Backend.Models.Database;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class PharmaceuticalFormDTO
    {
        //TODO: DO I NEED ANYTHING MORE?
        [JsonProperty("name")]
        public string Name { get; set; }

        public PharmaceuticalFormDTO(PharmaceuticalForm pharmaceuticalForm)
        {
            Name = pharmaceuticalForm.Name;
        }
    }
}
