using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.OrderEntity;

namespace Backend.Models.Database
{
    public enum OrderStateId
    {
        Created = 1,
        Approved,

        Ready,
        InTransit,
        Delivered,

        Returned
    }

    public class OrderState
    { 
        [Key]
        [Required]
        public OrderStateId Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
