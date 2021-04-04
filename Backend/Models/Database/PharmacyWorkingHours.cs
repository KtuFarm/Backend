namespace Backend.Models.Database
{
    public class PharmacyWorkingHours
    {
        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; }
        
        public int WorkingHoursId { get; set; }
        public WorkingHours WorkingHours { get; set; }
    }
}
