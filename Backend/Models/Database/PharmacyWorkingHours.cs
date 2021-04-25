using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Backend.Models.Common;

namespace Backend.Models.Database
{
    public class PharmacyWorkingHours: ISoftDeletable
    {
        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; }

        public int WorkingHoursId { get; set; }
        public WorkingHours WorkingHours { get; set; }
        
        [Required] 
        [DefaultValue(false)]
        public bool IsSoftDeleted { get; set; } = false;

        public PharmacyWorkingHours() { }

        public PharmacyWorkingHours(Pharmacy pharmacy, WorkingHours workingHours)
        {
            Pharmacy = pharmacy;
            WorkingHours = workingHours;
        }
    }
}
