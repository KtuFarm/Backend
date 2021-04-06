namespace Backend.Models.Database
{
    public class PharmacyWorkingHours
    {
        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; }

        public int WorkingHoursId { get; set; }
        public WorkingHours WorkingHours { get; set; }

        public PharmacyWorkingHours() { }

        public PharmacyWorkingHours(Pharmacy pharmacy, WorkingHours workingHours)
        {
            Pharmacy = pharmacy;
            WorkingHours = workingHours;
        }
    }
}
