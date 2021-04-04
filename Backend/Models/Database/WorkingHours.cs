using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Backend.Models.DTO;

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
        
        public ICollection<PharmacyWorkingHours> PharmacyWorkingHours { get; set; }

        public WorkingHours() { }
        
        public WorkingHours(CreateWorkingHoursDTO data)
        {
            OpenTime = TimeSpan.Parse(data.OpenTime);
            CloseTime = TimeSpan.Parse(data.CloseTime);
            DayOfWeekId = (DayOfWeekId)data.DayOfWeek;
        }
    }
}
