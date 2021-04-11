using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Database
{
    public class ProductBalance
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required] 
        public double Amount { get; set; } = 0;

        [Required] 
        public DateTime ExpirationDate { get; set; }
        
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
