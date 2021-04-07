using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Database
{
    public enum WorkerStateId
    {
        Working,
        OnVacation,
        Fired
    }

    public class WorkerState
    {
        [Key]
        [Required]
        public WorkerStateId Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<User> Workers { get; set; }
    }
}
