using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Common;
using Backend.Models.OrderEntity;
using Backend.Models.UserEntity;

namespace Backend.Models.Database
{
    public class OrderUser: ISoftDeletable
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsSoftDeleted { get; set; } = false;
    }
}
