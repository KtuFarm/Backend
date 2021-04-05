using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Backend.Models.DTO;

namespace Backend.Models.Database
{
    public class Pharmacy
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; init; }
        
        [Required]
        [StringLength(255)]
        public string City { get; init; }
        
        [Required]
        public ICollection<Register> Registers { get; set; }

        public ICollection<RequiredMedicamentAmount> RequiredMedicamentAmounts { get; set; }
        
        public ICollection<PharmacyWorkingHours> PharmacyWorkingHours { get; set; }

        public Pharmacy() { }
        
        public Pharmacy(CreatePharmacyDTO dto, IEnumerable<WorkingHours> workingHours)
        {
            Address = dto.Address;
            City = dto.City;

            PharmacyWorkingHours = CreateWorkingHours(workingHours);
        }

        private List<PharmacyWorkingHours> CreateWorkingHours(IEnumerable<WorkingHours> workingHours)
        {
            return workingHours.Select(hours => new PharmacyWorkingHours(this, hours)).ToList();
        }
    }
}
