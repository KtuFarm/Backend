using Backend.Models.Database;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Seed
{
    public class WorkingHoursSeed : ISeeder
    {
        private readonly ApiContext _context;

        public WorkingHoursSeed(ApiContext context)
        {
            _context = context;
        }

        public void EnsureCreated()
        {
            if (!ShouldSeed()) return;

            for (var day = DayOfWeekId.Monday; day < DayOfWeekId.Saturday; day++)
            {
                _context.Add(
                    new WorkingHours
                    {
                        OpenTime = new TimeSpan(9, 0, 0),
                        CloseTime = new TimeSpan(18, 0, 0),
                        DayOfWeekId = day
                    }
                );
            }

            _context.SaveChanges();
        }

        private bool ShouldSeed()
        {
            var hours = _context.WorkingHours.IgnoreQueryFilters().FirstOrDefault(m => m.Id == 1);
            return hours == null;
        }
    }
}
