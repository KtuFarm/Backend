using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Backend.Models.DTO;

namespace Backend.Models.Database
{
    public class Medicament: ISoftDeletable
    {
        [Key]
        [Required]
        public int Id { get; init; }

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
        [DefaultValue(0)]
        public double ReimbursePercentage { get; set; }
        
        [Required] 
        [DefaultValue(false)]
        public bool IsSoftDeleted { get; set; } = false;

        [Required]
        public PharmaceuticalFormId PharmaceuticalFormId { get; set; }

        [Required]
        public PharmaceuticalForm PharmaceuticalForm { get; set; }

        public int? ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public ICollection<RequiredMedicamentAmount> RequiredMedicamentAmounts { get; set; }

        public Medicament() {}

        public Medicament(CreateMedicamentDTO dto, PharmaceuticalForm pharmaceuticalForm)
        {
            Name = dto.Name;
            ActiveSubstance = dto.ActiveSubstance;
            BarCode = dto.BarCode;
            IsPrescriptionRequired = (bool) dto.IsPrescriptionRequired;
            IsReimbursed = (bool) dto.IsReimbursed;
            Country = dto.Country;
            BasePrice = (decimal) dto.BasePrice;
            Surcharge = (double) dto.Surcharge;
            ReimbursePercentage = (int) dto.ReimbursePercentage;
            PharmaceuticalForm = pharmaceuticalForm;
            PharmaceuticalFormId = pharmaceuticalForm.Id;

        }

        public void UpdateMedicamentFromDTO(EditMedicamentDTO dto)
        {
            IsPrescriptionRequired = (bool)dto.IsPrescriptionRequired;
            IsReimbursed = (bool)dto.IsReimbursed;
            BasePrice = (decimal)dto.BasePrice;
            Surcharge = (double)dto.Surcharge;
            ReimbursePercentage = (int)dto.ReimbursePercentage;
        }

        public decimal CalculateFullPrice()
        {
            decimal price = BasePrice * (100M + (decimal) Surcharge) / 100M;
            return Math.Round(price, 2);
        }

        public decimal CalculatePriceReimbursed()
        {
            decimal price = CalculateFullPrice();
            price *= (100M - (decimal) ReimbursePercentage) / 100M;
            
            return Math.Round(price, 2);
        }
    }
}
