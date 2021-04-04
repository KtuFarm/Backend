using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Database
{
    public class WorkingHours
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        public TimeSpan OpenTime { get; set; }
        
        [Required]
        public TimeSpan CloseTime { get; set; }
        
        [Required]
        public DayOfWeekId DayOfWeekId { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }
    }
}
