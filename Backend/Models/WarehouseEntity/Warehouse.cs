using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Backend.Models.Common;
using Backend.Models.OrderEntity;
using Backend.Models.ProductBalanceEntity;
using Backend.Models.UserEntity;

namespace Backend.Models.WarehouseEntity
{
    public class Warehouse : ISoftDeletable
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

        public ICollection<User> Employees { get; set; }

        public ICollection<ProductBalance> Products { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
