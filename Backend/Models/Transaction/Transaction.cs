using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Backend.Models.Common;

namespace Backend.Models.Database
{
    public class Transaction : ISoftDeletable
    {
        [Key] 
        [Required] 
        public int Id { get; init; }

        [Required] 
        public DateTime CreatedAt { get; set; }

        [Required] 
        public decimal PriceWithoutVat { get; set; } = 0.00M;

        [Required] 
        public decimal Vat { get; set; } = 0.00M;

        [Required] 
        public decimal TotalPrice { get; set; } = 0.00M;

        [Required] 
        public PaymentTypeId PaymentTypeId { get; set; }

        [Required] 
        public PaymentType PaymentType { get; set; }

        [Required] 
        public int RegisterId { get; set; }

        [Required] 
        public Register Register { get; set; }

        public int? UserId { get; set; }

        public User Pharmacist { get; set; }

        public int PharmacyId { get; set; }

        public Pharmacy Pharmacy { get; set; }

        public ICollection<ProductBalance> Medicaments { get; set; }

        [Required] 
        [DefaultValue(false)] 
        public bool IsSoftDeleted { get; set; }

        private const decimal VatAmount = 0.21M;

        public Transaction()
        {
            Medicaments = new List<ProductBalance>();
            CreatedAt = DateTime.Now;
        }
        
        public void AddProduct(ProductBalance pb, double amount)
        {
            Medicaments.Add(new ProductBalance
                {
                    Amount = amount,
                    Medicament = pb.Medicament,
                    ExpirationDate = pb.ExpirationDate,
                    Transaction = this
                }
            );

            decimal price = pb.Medicament.CalculatePriceReimbursed() * (decimal) amount;
            PriceWithoutVat += price;
            Vat += price * VatAmount;
            TotalPrice = PriceWithoutVat + Vat;
        }
    }
}
