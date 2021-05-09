using Newtonsoft.Json;

namespace Backend.Models.UserEntity.DTO
{
    public class UserDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        public UserDTO(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            Position = user.Position;
            Department = user.DepartmentId.ToString();
        }
    }
}
