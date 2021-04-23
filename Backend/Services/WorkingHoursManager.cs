using Backend.Models;
using Backend.Models.Database;
using Backend.Models.DTO;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class WorkingHoursManager : IWorkingHoursManager
    {
        private readonly ApiContext _context;

        public WorkingHoursManager(ApiContext context)
        {
            _context = context;
        }

        public List<WorkingHours> GetWorkingHoursFromDTO(List<WorkingHoursDTO> workingHoursData)
        {
            if (workingHoursData == null || workingHoursData.Count == 0)
            {
                return new List<WorkingHours>();
            }

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

        private static void ValidateDistinctDays(IEnumerable<WorkingHoursDTO> workingHoursData)
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

        public async Task<IEnumerable<WorkingHours>> GetPharmacyWorkingHours(int pharmacyId)
        {
            return await _context.PharmacyWorkingHours
                .Where(pwh => pwh.PharmacyId == pharmacyId)
                .Select(pwh => pwh.WorkingHours)
                .ToListAsync();
        }
    }
}
