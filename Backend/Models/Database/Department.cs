using Backend.Models.UserEntity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Database
{
    public enum DepartmentId
    {
        None = 1,
        Pharmacy,
        Warehouse,
        Admin,
        Transportation,
        Manufacturer
    }
    public class Department
    {
        [Key]
        [Required]
        public DepartmentId Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
