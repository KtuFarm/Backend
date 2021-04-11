using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Database
{
    public class Transaction: ISoftDeletable
    {
        [Key]
        [Required]
        public int Id { get; init; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public decimal PriceWithoutVat { get; set; }
        
        [Required]
        public decimal Vat { get; set; }
        
        [Required]
        public decimal TotalPrice { get; set; }
        
        [Required]
        public PaymentTypeId PaymentTypeId { get; set; }
        
        [Required]
        public PaymentType PaymentType { get; set; }
        
        [Required]
        [DefaultValue(false)]
        public bool IsSoftDeleted { get; set; }
        
        [Required]
        public int RegisterId { get; set; }
        
        [Required]
        public Register Register { get; set; }
        
        public int? UserId { get; set; }
        
        public User Pharmacist { get; set; }
        
        public int PharmacyId { get; set; }
        
        public Pharmacy Pharmacy { get; set; }
        
        public ICollection<ProductBalance> Medicaments { get; set; }
    }
}
