using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Backend.Models.Common;
using Backend.Models.Database;
using Backend.Models.OrderEntity.DTO;
using Backend.Models.PharmacyEntity;
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

        [Required]
        public ICollection<OrderProductBalance> OrderProductBalances { get; set; }

        [Required]
        public int WarehouseId { get; set; }

        [Required]
        public Warehouse Warehouse { get; set; }

        [Required]
        public int PharmacyId { get; set; }

        [Required]
        public Pharmacy Pharmacy { get; set; }

        [Required]
        public ICollection<OrderUser> OrderUsers { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsSoftDeleted { get; set; } = false;

        public Order() { }

        public Order(
            CreateOrderDTO dto,
            string addressFrom,
            string addressTo,
            int pharmacyId,
            IEnumerable<ProductBalance> productBalances
        )
        {
            AddressFrom = addressFrom;
            AddressTo = addressTo;
            CreationDate = DateTime.Now;
            DeliveryDate = DetermineDeliveryDate(CreationDate);
            OrderStateId = OrderStateId.Created;
            Total = CalculateTotalAmount(dto);
            WarehouseId = dto.WarehouseId;
            PharmacyId = pharmacyId;
            OrderProductBalances = productBalances.Select(pb => new OrderProductBalance(this, pb)).ToList();
        }

        public void UpdateFromDTO(CreateOrderDTO dto, List<ProductBalance> productBalances)
        {
            foreach (var product in productBalances)
            {
                OrderProductBalances.Add(new OrderProductBalance(this, product));
            }

            Total += CalculateTotalAmount(dto);
        }

        private DateTime DetermineDeliveryDate(DateTime creationDate)
        {
            return creationDate.Hour < 13
                ? new DateTime(CreationDate.Year, CreationDate.Month, CreationDate.Day + 2, 13, 0, 0)
                : new DateTime(CreationDate.Year, CreationDate.Month, CreationDate.Day + 3, 13, 0, 0);
        }

        private static double CalculateTotalAmount(CreateOrderDTO dto)
        {
            var products = dto.Products;
            return products?.Aggregate(0.0, (total, next) => total + next.Amount) ?? 0.0;
        }

        public bool IsAuthorized(User user)
        {
            return PharmacyId == user.PharmacyId || WarehouseId == user.WarehouseId;
        }

        public bool IsCreatedToday(CreateOrderDTO dto, int pharmacyId)
        {
            return WarehouseId == dto.WarehouseId
                   && PharmacyId == pharmacyId
                   && OrderStateId == OrderStateId.Created
                   && CreationDate.Date == DateTime.Now.Date;
        }
    }
}
