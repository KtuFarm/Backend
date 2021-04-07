using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Database
{
    public class User
    {
        [Key]
        [Required]
        public int Uid { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Surname { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        [Required]
        public WorkerState State { get; set; }

        [Required]
        public DateTime DismissalDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Position { get; set; }
    }
}
