using Newtonsoft.Json;
using System;

namespace Backend.Models.ReportEntity.DTO
{
    public class ReportFullDTO : ReportDTO
    {
        [JsonProperty("orderCount")]
        public int OrderCount { get; set; }

        [JsonProperty("transactionCount")]
        public int TransactionCount { get; set; }

        [JsonProperty("totalRevenue")]
        public decimal TotalRevenue { get; set; } = 0.00M;

        [JsonProperty("revenueInCash")]
        public decimal RevenueInCash { get; set; } = 0.00M;

        [JsonProperty("generationDate")]
        public DateTime GenerationDate { get; set; }

        [JsonProperty("totalOrderAmount")]
        public decimal TotalOrderAmount { get; set; } = 0.00M;

        [JsonProperty("profit")]
        public decimal Profit { get; set; } = 0.00M;

        [JsonProperty("reportType")]
        public string ReportType { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("pharmacy")]
        public string Pharmacy { get; set; }

        public ReportFullDTO(Report report) : base(report)
        {
            OrderCount = report.OrderCount;
            TransactionCount = report.TransactionCount;
            TotalRevenue = report.TotalRevenue;
            RevenueInCash = report.RevenueInCash;
            GenerationDate = report.GenerationDate;
            TotalOrderAmount = report.TotalOrderAmount;
            Profit = report.Profit;
            ReportType = report.ReportTypeId.ToString();
            User = $"{report.User.Name} {report.User.Surname}";
            Pharmacy = report.Pharmacy.Address;
        }
    }
}
