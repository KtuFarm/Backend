using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Backend.Models.WorkingHoursEntity;

namespace Backend.Models.Database
{
    public enum DayOfWeekId
    {
        Monday = 1,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public class DayOfWeek
    {
        [Key]
        [Required]
        public DayOfWeekId Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<WorkingHours> WorkingHours { get; set; }
    }
}
