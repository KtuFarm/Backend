using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Backend.Models.Database
{
    public class RequiredMedicamentAmount
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public Pharmacy Pharmacy { get; set; }

        [Required]
        public Medicament Medicament { get; set; }
    }
}
