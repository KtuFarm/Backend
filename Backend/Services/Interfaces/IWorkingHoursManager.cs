using Backend.Models.Database;
using Backend.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services.Interfaces
{
    public interface IWorkingHoursManager
    {
        public List<WorkingHours> GetWorkingHoursFromDTO(List<WorkingHoursDTO> workingHoursData);

        public Task<IEnumerable<WorkingHours>> GetPharmacyWorkingHours(int pharmacyId);
    }
}
