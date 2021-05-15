using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Backend.Models.OrderEntity;

namespace Backend.Models.Database
{
    public enum OrderStateId
    {
        Created = 1,
        Approved,
        InPreparation,
        Ready,
        InTransit,
        Delivered,
        Returning,
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
