﻿using Backend.Models.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Backend.Models.Database
{
    public class Pharmacy
    {
        [Key]
        [Required]
        public int Id { get; init; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [StringLength(255)]
        public string City { get; set; }

        [Required]
        public ICollection<Register> Registers { get; set; }

        public ICollection<RequiredMedicamentAmount> RequiredMedicamentAmounts { get; set; }

        public ICollection<PharmacyWorkingHours> PharmacyWorkingHours { get; set; }

        public ICollection<User> Pharmacists { get; set; }

        public Pharmacy() { }

        public Pharmacy(CreatePharmacyDTO dto, IEnumerable<WorkingHours> workingHours)
        {
            Address = dto.Address;
            City = dto.City;

            UpdateWorkingHours(workingHours);
        }

        public void UpdateWorkingHours(IEnumerable<WorkingHours> workingHours)
        {
            PharmacyWorkingHours?.Clear();
            PharmacyWorkingHours = workingHours.Select(hours => new PharmacyWorkingHours(this, hours)).ToList();
        }
    }
}
