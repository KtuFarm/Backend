using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Backend.Models.Common;
using Backend.Models.Database;
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

        [Required]
        public ICollection<OrderProductBalance> OrderProductBalances { get; set; }

        [Required]
        public int WarehouseId { get; set; }

        [Required]
        public Warehouse Warehouse { get; set; }

        [Required]
        public ICollection<OrderUser> OrderUsers { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsSoftDeleted { get; set; } = false;
    }
}
