using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Backend.Models.Common;
using Backend.Models.Database;
using Backend.Models.PharmacyEntity.DTO;
using Backend.Models.ProductBalanceEntity;
using Backend.Models.RegisterEntity;
using Backend.Models.TransactionEntity;
using Backend.Models.UserEntity;
using Backend.Models.WorkingHoursEntity;

namespace Backend.Models.PharmacyEntity
{
    public class Pharmacy : ISoftDeletable
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
        [DefaultValue(false)]
        public bool IsSoftDeleted { get; set; } = false;

        [Required]
        public ICollection<Register> Registers { get; set; }

        public ICollection<RequiredMedicamentAmount> RequiredMedicamentAmounts { get; set; }

        public ICollection<PharmacyWorkingHours> PharmacyWorkingHours { get; set; }

        public ICollection<User> Pharmacists { get; set; }

        public ICollection<ProductBalance> Products { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public Pharmacy() { }

        public Pharmacy(CreatePharmacyDTO dto, IEnumerable<WorkingHours> workingHours)
        {
            Address = dto.Address;
            City = dto.City;

            CreateRegisters(dto.RegistersCount);
            UpdateWorkingHours(workingHours);
        }

        private void CreateRegisters(int count)
        {
            Registers = new List<Register>();
            for (int i = 0; i < count; i++)
            {
                Registers.Add(new Register(this));
            }
        }

        public void UpdateFromDTO(EditPharmacyDTO dto, IEnumerable<WorkingHours> workingHours)
        {
            Address = (dto.Address ?? Address);
            City = (dto.City ?? City);

            if (dto.WorkingHours != null)
            {
                UpdateWorkingHours(workingHours);
            }
        }

        private void UpdateWorkingHours(IEnumerable<WorkingHours> workingHours)
        {
            PharmacyWorkingHours?.Clear();
            PharmacyWorkingHours = workingHours.Select(hours => new PharmacyWorkingHours(this, hours)).ToList();
        }
    }
}
