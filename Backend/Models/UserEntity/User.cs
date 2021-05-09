using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Backend.Models.Common;
using Backend.Models.Database;
using Backend.Models.ManufacturerEntity;
using Backend.Models.OrderEntity;
using Backend.Models.PharmacyEntity;
using Backend.Models.TransactionEntity;
using Backend.Models.UserEntity.DTO;
using Backend.Models.WarehouseEntity;

namespace Backend.Models.UserEntity
{
    public class User : ISoftDeletable
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

        [Required]
        [DefaultValue("")]
        [StringLength(255)]
        public string Email { get; set; } = "";

        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [DefaultValue(DepartmentId.None)]
        public DepartmentId DepartmentId { get; set; } = DepartmentId.None;

        public Department Department { get; set; }

        public DateTime? DismissalDate { get; set; } = null;

        [Required]
        [StringLength(255)]
        public string Position { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsSoftDeleted { get; set; } = false;

        public int? PharmacyId { get; set; }

        public Pharmacy Pharmacy { get; set; }

        public int? WarehouseId { get; set; }

        public Warehouse Warehouse { get; set; }

        public IEnumerable<Manufacturer> Manufacturers { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public ICollection<OrderUser> OrderUsers { get; set; }

        public User() { }

        public User(CreateUserDTO dto)
        {
            Email = dto.Email;
            Username = $"{dto.Name}_{dto.Surname}";

            Name = dto.Name;
            Surname = dto.Surname;
            Position = dto.Position;
            RegistrationDate = DateTime.Now;
            DepartmentId = (DepartmentId) dto.DepartmentId;

            SetWorkplace(dto);
        }

        private void SetWorkplace(CreateUserDTO dto)
        {
            var departmentId = (DepartmentId) dto.DepartmentId;
            switch (departmentId)
            {
                case DepartmentId.Pharmacy:
                    PharmacyId = dto.PharmacyId;
                    break;
                case DepartmentId.Warehouse:
                    WarehouseId = dto.WarehouseId;
                    break;
            }
        }

        public void UpdateFromDTO(EditUserDTO dto)
        {
            Name = dto.Name ?? Name;
            Surname = dto.Surname ?? Surname;
            Position = dto.Position ?? Position;
            PharmacyId = dto.PharmacyId ?? PharmacyId;
            DismissalDate = dto.DismissalDate ?? DismissalDate;
        }
    }
}
