using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Backend.Models.DTO;

namespace Backend.Models.Database
{
    public class WorkingHours: IEquatable<WorkingHours>
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        public TimeSpan OpenTime { get; init; }
        
        [Required]
        public TimeSpan CloseTime { get; init; }
        
        [Required]
        public DayOfWeekId DayOfWeekId { get; init; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }
        
        public ICollection<PharmacyWorkingHours> PharmacyWorkingHours { get; set; }

        public WorkingHours() { }
        
        public WorkingHours(WorkingHoursDTO dto)
        {
            var (openTime, closeTime) = ValidateTime(dto);
            OpenTime = openTime;
            CloseTime = closeTime;

            DayOfWeekId = ValidateDay(dto);
        }
        
        private static DayOfWeekId ValidateDay(WorkingHoursDTO dto)
        {
            var days = Enum.GetValues(typeof(DayOfWeekId)).Cast<DayOfWeekId>().ToList();
            if (!days.Contains((DayOfWeekId) dto.DayOfWeek))
            {
                throw new ArgumentException("Invalid day of the week!");
            }

            return (DayOfWeekId) dto.DayOfWeek;
        }

        private static (TimeSpan openTime, TimeSpan closeTime) ValidateTime(WorkingHoursDTO dto)
        {
            bool isInvalid = !TimeSpan.TryParse(dto.OpenTime, out var openTime);
            isInvalid |= !TimeSpan.TryParse(dto.CloseTime, out var closeTime);

            if (isInvalid || (closeTime <= openTime))
            {
                throw new ArgumentException("Invalid working hours!");
            }

            return (openTime, closeTime);
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
            
            return obj.GetType() == GetType() && Equals((WorkingHours) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OpenTime, CloseTime, (int) DayOfWeekId);
        }
    }
}
