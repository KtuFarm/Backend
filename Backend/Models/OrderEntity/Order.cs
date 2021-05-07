using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Common;
using Backend.Models.Database;
using Backend.Models.ProductBalanceEntity;
using Backend.Models.UserEntity;
using Backend.Models.WarehouseEntity;

namespace Backend.Models.OrderEntity
{
    public class Order : ISoftDeletable
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        [StringLength(255)]
        public string AddressFrom { get; set; }

        [StringLength(255)]
        [Required]
        public string AddressTo { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        public OrderStateId OrderStateId { get; set; }

        [Required]
        public OrderState OrderState { get; set; }

        [Required]
        public double Total { get; set; }

        public int? WarehouseId { get; set; }

        [Required]
        public Warehouse Warehouse { get; set; }

        public int? PharmacistId { get; set; }

        [Required]
        public User Pharmacist { get; set; }

        public int? CourierId { get; set; }

        [Required]
        public User Courier { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsSoftDeleted { get; set; } = false;
    }
}
