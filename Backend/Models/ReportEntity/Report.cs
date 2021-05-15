using Backend.Models.Common;
using Backend.Models.Database;
using Backend.Models.PharmacyEntity;
using Backend.Models.UserEntity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.ReportEntity
{
    public class Report : ISoftDeletable
    {
        [Key]
        [Required]
        public int Id { get; init; }

        [Required]
        [StringLength(255)]
        public string Code { get; set; }

        [Required]
        public DateTime DateFrom { get; set; }

        [Required]
        public DateTime DateTo { get; set; }

        [Required]
        public int OrderCount { get; set; }

        [Required]
        public int TransactionCount { get; set; }

        [Required]
        public decimal TotalRevenue { get; set; } = 0.00M;

        [Required]
        public decimal RevenueInCash { get; set; } = 0.00M;

        [Required]
        public DateTime GenerationDate { get; set; }

        [Required]
        public decimal TotalOrderAmount { get; set; } = 0.00M;

        [Required]
        public decimal Profit { get; set; }

        [Required]
        public ReportTypeId ReportTypeId { get; set; }

        [Required]
        public ReportType ReportType { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int PharmacyId { get; set; }

        public Pharmacy Pharmacy { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsSoftDeleted { get; set; } = false;
    }
}
