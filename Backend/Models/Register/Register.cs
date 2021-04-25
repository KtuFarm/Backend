using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Backend.Models.Common;

namespace Backend.Models.Database
{
    public class Register: ISoftDeletable
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [DefaultValue(0.00)]
        public decimal Cash { get; set; } = 0.00M;
        
        [Required] 
        [DefaultValue(false)]
        public bool IsSoftDeleted { get; set; } = false;

        [Required]
        public int PharmacyId { get; set; }

        [Required]
        public Pharmacy Pharmacy { get; set; }
        
        public ICollection<Transaction> Transactions { get; set; }

        public Register() { }

        public Register(Pharmacy pharmacy)
        {
            Pharmacy = pharmacy;
        }
    }
}
