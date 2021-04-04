using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
