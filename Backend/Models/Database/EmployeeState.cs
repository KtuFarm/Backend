using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Database
{
    public enum EmployeeStateId
    {
        Working = 1,
        OnVacation,
        Fired
    }

    public class EmployeeState
    {
        [Key]
        [Required]
        public EmployeeStateId Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<User> Workers { get; set; }
    }
}
