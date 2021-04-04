using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Backend.Models.DTO;

namespace Backend.Models.Database
{
    public class WorkingHours: IEquatable<WorkingHours>
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
        
        public bool Equals(WorkingHours other)
        {
            if (ReferenceEquals(null, other)) return false;
            
            return ReferenceEquals(this, other) || IsEqual(other);
        }

        private bool IsEqual(WorkingHours other)
        {
            return OpenTime.Equals(other.OpenTime) && 
                   CloseTime.Equals(other.CloseTime) && 
                   DayOfWeekId == other.DayOfWeekId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            
            return obj.GetType() == this.GetType() && Equals((WorkingHours) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OpenTime, CloseTime, (int) DayOfWeekId);
        }
    }
}
