using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Backend.Models.Common;
using Backend.Models.OrderEntity;
using Backend.Models.ProductBalanceEntity;

namespace Backend.Models.Database
{
    public class OrderProductBalance : ISoftDeletable
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductBalanceId { get; set; }
        public ProductBalance ProductBalance { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsSoftDeleted { get; set; } = false;

        public OrderProductBalance() { }

        public OrderProductBalance(Order order, ProductBalance pb)
        {
            Order = order;
            ProductBalance = pb;
        }
    }
}
