using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Backend.Models.Common;
using Backend.Models.MedicamentEntity;
using Backend.Models.PharmacyEntity;

namespace Backend.Models.Database
{
    public class RequiredMedicamentAmount: ISoftDeletable
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public double Amount { get; set; }
        
        [Required] 
        [DefaultValue(false)]
        public bool IsSoftDeleted { get; set; } = false;

        [Required]
        public int PharmacyId { get; set; }
        
        [Required]
        public Pharmacy Pharmacy { get; set; }

        [Required]
        public int MedicamentId { get; set; }
        
        [Required]
        public Medicament Medicament { get; set; }
    }
}
