using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Database
{
    public class Medicament
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string ActiveSubstance { get; set; }

        [Required]
        [StringLength(255)]
        public string BarCode { get; set; }

        [Required]
        public bool IsPrescriptionRequired { get; set; }

        [Required]
        public bool IsReimbursed { get; set; }

        [Required]
        [StringLength(255)]
        public string Country { get; set; }


        [Required]
        public decimal BasePrice { get; set; }

        [Required]
        public double Surcharge { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool IsSellable { get; set; } = true;

        [Required]
        [DefaultValue(0)]
        public int ReimbursePercentage { get; set; } = 0;

        [Required]
        public PharmaceuticalFormId PharmaceuticalFormId { get; set; }

        [Required]
        public PharmaceuticalForm PharmaceuticalForm { get; set; }
    }
}
