using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Backend.Models.MedicamentEntity;
using Backend.Models.UserEntity;

namespace Backend.Models.Database
{
    public class Manufacturer
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }

        public ICollection<Medicament> Medicaments { get; set; }

        public User Supplier { get; set; }

        public int? SupplierId { get; set; }
    }
}
