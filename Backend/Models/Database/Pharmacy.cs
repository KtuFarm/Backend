using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Database
{
    public class Pharmacy
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }


        [Required]
        [StringLength(255)]
        public string City { get; set; }


        [Required]
        public ICollection<Register> Registers { get; set; }

        public ICollection<RequiredMedicamentAmount> RequiredMedicamentAmounts { get; set; }
        
        public ICollection<PharmacyWorkingHours> PharmacyWorkingHours { get; set; }
    }
}
