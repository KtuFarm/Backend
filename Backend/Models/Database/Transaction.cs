using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Database
{
    public class Transaction
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public decimal PriceWithoutVat { get; set; }
        
        [Required]
        public decimal Vat { get; set; }
        
        [Required]
        public decimal TotalPrice { get; set; }
        
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
