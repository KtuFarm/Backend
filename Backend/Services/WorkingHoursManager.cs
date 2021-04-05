using System;
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

        public List<WorkingHours> GetWorkingHoursFromDTO(List<CreateWorkingHoursDTO> workingHoursData)
        {
            ValidateDistinctDays(workingHoursData);

            var list = new List<WorkingHours>();
            foreach (var workingHoursDTO in workingHoursData)
            {
                var newHours = new WorkingHours(workingHoursDTO);
                var existingHours = _context.WorkingHours
                    .AsEnumerable()
                    .FirstOrDefault(hours => hours.Equals(newHours));

                list.Add(SelectHours(existingHours, newHours));
            }

            return list;
        }

        private static void ValidateDistinctDays(IEnumerable<CreateWorkingHoursDTO> workingHoursData)
        {
            var days = workingHoursData.Select(dto => dto.DayOfWeek).ToList();
            if (days.Distinct().Count() != days.Count)
            {
                throw new ArgumentException("Days of week should be unique!");
            }
        }

        private WorkingHours SelectHours(WorkingHours existingHours, WorkingHours newHours)
        {
            if (existingHours != null) return existingHours;
            
            _context.Add(newHours);
            _context.SaveChanges();

            return newHours;
        }
    }
}
