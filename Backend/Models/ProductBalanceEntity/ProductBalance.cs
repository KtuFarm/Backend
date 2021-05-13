using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Backend.Models.Common;
using Backend.Models.Database;
using Backend.Models.DTO;
using Backend.Models.MedicamentEntity;
using Backend.Models.PharmacyEntity;
using Backend.Models.TransactionEntity;
using Backend.Models.WarehouseEntity;

namespace Backend.Models.ProductBalanceEntity
{
    public class ProductBalance : ISoftDeletable
    {
        [Key]
        [Required]
        public int Id { get; init; }

        [Required]
        public double Amount { get; set; } = 0;

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsSoftDeleted { get; set; } = false;

        [Required]
        public int MedicamentId { get; set; }

        [Required]
        public Medicament Medicament { get; set; }

        public int? PharmacyId { get; set; }

        public Pharmacy Pharmacy { get; set; }

        public int? TransactionId { get; set; }

        public Transaction Transaction { get; set; }

        public int? WarehouseId { get; set; }

        public Warehouse Warehouse { get; set; }

        public ICollection<OrderProductBalance> OrderProductBalances { get; set; }

        public ProductBalance() { }

        public ProductBalance(TransactionProductDTO pb)
        {
            Amount = pb.Amount;
        }

        public ProductBalance(ProductBalance pb, double amount)
        {
            Amount = amount;
            ExpirationDate = pb.ExpirationDate;
            MedicamentId = pb.MedicamentId;
            PharmacyId = pb.PharmacyId;
            TransactionId = pb.TransactionId;
            WarehouseId = pb.WarehouseId;
        }
    }
}
