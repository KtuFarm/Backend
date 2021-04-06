using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Backend.Models.DTO;

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

        public int? ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public ICollection<RequiredMedicamentAmount> RequiredMedicamentAmounts { get; set; }


        public Medicament(CreateMedicamentDTO dto, PharmaceuticalForm pharmaceuticalForm)
        {
            Name = dto.Name;
            ActiveSubstance = dto.ActiveSubstance;
            BarCode = dto.BarCode;
            IsPrescriptionRequired = dto.IsPrescriptionRequired;
            IsReimbursed = dto.IsReimbursed;
            Country = dto.Country;
            BasePrice = dto.BasePrice;
            Surcharge = dto.Surcharge;
            IsSellable = dto.IsSellable;
            ReimbursePercentage = dto.ReimbursePercentage;
            PharmaceuticalForm = pharmaceuticalForm;
            PharmaceuticalFormId = pharmaceuticalForm.Id;

        }
    }
}
