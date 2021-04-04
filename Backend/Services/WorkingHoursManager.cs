using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Backend.Models.Database;
using Backend.Models.DTO;
using Backend.Services.Interfaces;

namespace Backend.Services
{
    public class WorkingHoursManager: IWorkingHoursManager
    {
        private readonly ApiContext _context;

        public WorkingHoursManager(ApiContext context)
        {
            _context = context;
        }

        public List<WorkingHours> GetWorkingHoursFromDTO(IEnumerable<CreateWorkingHoursDTO> workingHoursData)
        {
            return CreateWorkingHours(workingHoursData);
        }
        
        private List<WorkingHours> CreateWorkingHours(IEnumerable<CreateWorkingHoursDTO> workingHoursData)
        {
            var allWorkingHours = _context.WorkingHours.ToList();
            
            var list = new List<WorkingHours>();
            foreach (var workingHoursDTO in workingHoursData)
            {
                var wh = new WorkingHours(workingHoursDTO);
                list.Add(wh);
            }

            return list;
        }
    }
}
