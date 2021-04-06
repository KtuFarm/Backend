using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Database
{
    public class Register
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [DefaultValue(0.00)]
        public decimal Cash { get; set; } = 0.00M;

        [Required]
        public int PharmacyId { get; set; }

        [Required]
        public Pharmacy Pharmacy { get; set; }

        public Register() { }

        public Register(Pharmacy pharmacy)
        {
            Pharmacy = pharmacy;
        }
    }
}
