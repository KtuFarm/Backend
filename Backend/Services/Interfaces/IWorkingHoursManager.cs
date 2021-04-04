using System.Collections.Generic;
using Backend.Models.Database;
using Backend.Models.DTO;

namespace Backend.Services.Interfaces
{
    public interface IWorkingHoursManager
    {
        public List<WorkingHours> GetWorkingHoursFromDTO(IEnumerable<CreateWorkingHoursDTO> workingHoursData);
    }
}
