using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Backend.Models.Common;
using Backend.Models.MedicamentEntity;
using Backend.Models.PharmacyEntity;
using Backend.Models.TransactionEntity;

namespace Backend.Models.ProductBalanceEntity
{
    public class ProductBalance: ISoftDeletable
    {
        [Key]
        [Required]
        public int Id { get; init; }

        [Required] 
        public double Amount { get; set; } = 0;

        [Required] 
        public DateTime ExpirationDate { get; set; }
        
        [Required]
        [DefaultValue(false)]
        public bool IsSoftDeleted { get; set; }
        
        [Required]
        public int MedicamentId { get; set; }

        [Required] 
        public Medicament Medicament { get; set; }
        
        public int? PharmacyId { get; set; }

        public Pharmacy Pharmacy { get; set; }
        
        public int? TransactionId { get; set; }
        
        public Transaction Transaction { get; set; }
    }
}
