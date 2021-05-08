using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.OrderEntity;
using Backend.Models.UserEntity;

namespace Backend.Models.Database
{
    public class OrderUser
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
