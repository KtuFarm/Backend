using Backend.Models.Common;
using Newtonsoft.Json;

namespace Backend.Models.UserEntity.DTO
{
    public class GetUserDTO
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        public GetUserDTO(User user)
        {
            Meta = new Meta();
            Name = user.Name;
            Surname = user.Surname;
            Position = user.Position;
        }
    }
}
