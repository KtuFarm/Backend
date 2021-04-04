using System;
using System.Linq;
using Backend.Models.Database;

namespace Backend.Models.Seed
{
    public static class WorkingHoursSeed
    {
        public static void EnsureCreated(ApiContext context)
        {
            var workingHours = context.WorkingHours.FirstOrDefault(wh => wh.Id == 1);
            if (workingHours != null) return;

            for (var day = DayOfWeekId.Monday; day < DayOfWeekId.Saturday; day++)
            {
                context.Add(
                    new WorkingHours()
                    {
                        OpenTime = new TimeSpan(9, 0, 0),
                        CloseTime = new TimeSpan(18, 0, 0),
                        DayOfWeekId = day
                    });
            }
        }
    }
}
