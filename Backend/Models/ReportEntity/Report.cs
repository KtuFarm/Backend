using Backend.Models.Common;
using Backend.Models.Database;
using Backend.Models.PharmacyEntity;
using Backend.Models.ReportEntity.DTO;
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
        public decimal TotalOrderSum { get; set; } = 0.00M;

        [Required]
        public decimal Profit { get; set; } = 0.00M;

        [Required]
        public ReportTypeId ReportTypeId { get; set; }

        [Required]
        public ReportType ReportType { get; set; }

        public int UserId { get; set; }

        [Required]
        public User User { get; set; }

        public int PharmacyId { get; set; }

        [Required]
        public Pharmacy Pharmacy { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsSoftDeleted { get; set; } = false;

        public Report() { }

        public Report(CreateReportDTO dto, int pharmacyId, int userId, int orderCount, int transactionCount, decimal totalRevenue, decimal revenueInCash, decimal totalOrderSum)
        {
            DateFrom = dto.DateFrom;
            DateTo = dto.DateTo;
            ReportTypeId = (ReportTypeId)dto.ReportTypeId;
            GenerationDate = DateTime.Now;
            Code = GenerateCode(pharmacyId);
            OrderCount = orderCount;
            TransactionCount = transactionCount;
            TotalRevenue = totalRevenue;
            RevenueInCash = revenueInCash;
            TotalOrderSum = totalOrderSum;
            Profit = TotalRevenue - TotalOrderSum;
            UserId = userId;
            PharmacyId = pharmacyId;
        }

        private string GenerateCode(int pharmacyId)
        {
            string code = $"REP_{pharmacyId}_{GenerationDate.Year}_";
            code += GetTwoDigitCode(GenerationDate.Month) + "_";
            code += GetTwoDigitCode(GenerationDate.Day) + $"_{Id}";
            return code;
        }

        private string GetTwoDigitCode(int number)
        {
            return number < 10 ? $"0{number}" : number.ToString();
        }
    }
}
