using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Database
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Surname { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        [Required]
        public WorkerStateId WorkerStateId { get; set; }

        [Required]
        public WorkerState WorkerState { get; set; }

        public DateTime? DismissalDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Position { get; set; }

        [Required]
        public int PharmacyId { get; set; }

        [Required]
        public Pharmacy Pharmacy { get; set; }

        public ICollection<Manufacturer> Manufacturers { get; set; }
    }
}
