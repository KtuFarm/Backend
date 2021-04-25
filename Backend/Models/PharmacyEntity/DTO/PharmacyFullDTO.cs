using System.Collections.Generic;
using System.Linq;
using Backend.Models.RegisterEntity;
using Backend.Models.RegisterEntity.DTO;
using Backend.Models.WorkingHoursEntity;
using Backend.Models.WorkingHoursEntity.DTO;
using Newtonsoft.Json;

namespace Backend.Models.PharmacyEntity.DTO
{
    public class PharmacyFullDTO : PharmacyDTO
    {
        [JsonProperty("workingHours")]
        public IEnumerable<WorkingHoursDTO> WorkingHours { get; set; }
        
        [JsonProperty("registers")]
        public IEnumerable<RegisterDTO> Registers { get; set; }

        public PharmacyFullDTO(Pharmacy pharmacy, IEnumerable<WorkingHours> workingHours) : base(pharmacy)
        {
            WorkingHours = SerializeWorkingHours(workingHours);
            Registers = SerializeRegisters(pharmacy.Registers);
        }

        private static IEnumerable<WorkingHoursDTO> SerializeWorkingHours(IEnumerable<WorkingHours> workingHours)
        {
            return workingHours.Select(hour => new WorkingHoursDTO(hour)).ToList();
        }

        private static IEnumerable<RegisterDTO> SerializeRegisters(IEnumerable<Register> registers)
        {
            return registers.Select(r => new RegisterDTO(r)).ToList();
        }
    }
}
