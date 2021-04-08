﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Database
{
    public class User: ISoftDeletable
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
        public EmployeeStateId EmployeeStateId { get; set; } = EmployeeStateId.Working;

        [Required]
        public EmployeeState EmployeeState { get; set; }

        public DateTime? DismissalDate { get; set; } = null;

        [Required]
        [StringLength(255)]
        public string Position { get; set; }

        [Required] 
        [DefaultValue(false)]
        public bool IsSoftDeleted { get; set; } = false;

        [Required]
        public int PharmacyId { get; set; }

        [Required]
        public Pharmacy Pharmacy { get; set; }

        public ICollection<Manufacturer> Manufacturers { get; set; }
    }
}
