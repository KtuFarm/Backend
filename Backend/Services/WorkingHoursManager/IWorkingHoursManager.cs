using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.WorkingHoursEntity;
using Backend.Models.WorkingHoursEntity.DTO;

namespace Backend.Services.WorkingHoursManager
{
    public interface IWorkingHoursManager
    {
        public List<WorkingHours> GetWorkingHoursFromDTO(List<WorkingHoursDTO> workingHoursData);

        public Task<IEnumerable<WorkingHours>> GetPharmacyWorkingHours(int pharmacyId);
    }
}
