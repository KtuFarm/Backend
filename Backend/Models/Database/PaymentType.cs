using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Backend.Models.TransactionEntity;

namespace Backend.Models.Database
{
    public enum PaymentTypeId
    {
        Cash = 1,
        Card,
        Other
    }
    
    public class PaymentType
    {
        [Key]
        [Required]
        public PaymentTypeId Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public ICollection<Transaction> Transactions { get; set; }
    }
}
