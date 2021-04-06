using Backend.Models.Database;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Models.DTO
{
    public class PharmacyFullDTO : PharmacyDTO
    {
        [JsonProperty("workingHours")]
        public IEnumerable<WorkingHoursDTO> WorkingHours { get; set; }

        public PharmacyFullDTO(Pharmacy pharmacy, IEnumerable<WorkingHours> workingHours) : base(pharmacy)
        {
            WorkingHours = SerializeWorkingHours(workingHours);
        }

        private static IEnumerable<WorkingHoursDTO> SerializeWorkingHours(IEnumerable<WorkingHours> workingHours)
        {
            return workingHours.Select(hour => new WorkingHoursDTO(hour)).ToList();
        }
    }
}
