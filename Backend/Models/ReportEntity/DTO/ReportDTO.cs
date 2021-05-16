using Newtonsoft.Json;
using System;

namespace Backend.Models.ReportEntity.DTO
{
    public class ReportDTO
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("dateFrom")]
        public DateTime DateFrom { get; set; }

        [JsonProperty("dateTo")]
        public DateTime DateTo { get; set; }

        public ReportDTO(Report report)
        {
            Code = report.Code;
            DateFrom = report.DateFrom;
            DateTo = report.DateTo;
        }
    }
}
