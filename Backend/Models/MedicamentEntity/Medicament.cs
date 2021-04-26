using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Backend.Models.Common;
using Backend.Models.Database;
using Backend.Models.ManufacturerEntity;
using Backend.Models.MedicamentEntity.DTO;
using Backend.Models.ProductBalanceEntity;

namespace Backend.Models.MedicamentEntity
{
    public class Medicament : ISoftDeletable, IEquatable<Medicament>
    {
        [Key]
        [Required]
        public int Id { get; init; }

        [Required]
        [StringLength(255)]
        public string Name { get; init; }

        [Required]
        [StringLength(255)]
        public string ActiveSubstance { get; init; }

        [Required]
        [StringLength(255)]
        public string BarCode { get; init; }

        [Required]
        public bool IsPrescriptionRequired { get; set; }

        [Required]
        public bool IsReimbursed { get; set; }

        [Required]
        [StringLength(255)]
        public string Country { get; init; }

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

        public ICollection<ProductBalance> Balances { get; set; }

        public Medicament() { }

        public Medicament(CreateMedicamentDTO dto)
        {
            Name = dto.Name;
            ActiveSubstance = dto.ActiveSubstance;
            BarCode = dto.BarCode;
            IsPrescriptionRequired = dto.IsPrescriptionRequired;
            IsReimbursed = dto.IsReimbursed;
            Country = dto.Country;
            BasePrice = dto.BasePrice;
            Surcharge = dto.Surcharge;
            ReimbursePercentage = dto.ReimbursePercentage ?? 0;
            PharmaceuticalFormId = (PharmaceuticalFormId) dto.PharmaceuticalFormId;
        }

        public void UpdateFromDTO(EditMedicamentDTO dto)
        {
            IsPrescriptionRequired = (dto.IsPrescriptionRequired ?? IsPrescriptionRequired);
            IsReimbursed = (dto.IsReimbursed ?? IsReimbursed);
            BasePrice = (dto.BasePrice ?? BasePrice);
            Surcharge = (dto.Surcharge ?? Surcharge);
            ReimbursePercentage = (dto.ReimbursePercentage ?? ReimbursePercentage);
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

        public bool Equals(Medicament other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Name == other.Name &&
                   ActiveSubstance == other.ActiveSubstance &&
                   BarCode == other.BarCode &&
                   IsPrescriptionRequired == other.IsPrescriptionRequired &&
                   IsReimbursed == other.IsReimbursed &&
                   Country == other.Country &&
                   BasePrice == other.BasePrice &&
                   Surcharge.Equals(other.Surcharge) &&
                   ReimbursePercentage.Equals(other.ReimbursePercentage) &&
                   PharmaceuticalFormId == other.PharmaceuticalFormId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Medicament) obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Name);
            hashCode.Add(ActiveSubstance);
            hashCode.Add(BarCode);
            hashCode.Add(IsPrescriptionRequired);
            hashCode.Add(IsReimbursed);
            hashCode.Add(Country);
            hashCode.Add(BasePrice);
            hashCode.Add(Surcharge);
            hashCode.Add(ReimbursePercentage);
            hashCode.Add((int) PharmaceuticalFormId);
            return hashCode.ToHashCode();
        }
    }
}
