using Backend.Models.Database;
using Newtonsoft.Json;
using System;

namespace Backend.Models.DTO
{
    public class UserDTO
    {
        [JsonProperty("uid")]
        public int Uid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("registrationDate")]
        public DateTime RegistrationDate { get; set; }

        [JsonProperty("dismissalDate")]
        public DateTime? DismissalDate { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        public UserDTO(User user)
        {
            Uid = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            RegistrationDate = user.RegistrationDate;
            DismissalDate = user.DismissalDate;
            Position = user.Position;
            State = user.WorkerState.Name;
        }
    }
}
