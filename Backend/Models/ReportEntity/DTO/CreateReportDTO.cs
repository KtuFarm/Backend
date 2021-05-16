using Newtonsoft.Json;
using System;

namespace Backend.Models.ReportEntity.DTO
{
    public class CreateReportDTO
    {
        [JsonProperty("dateFrom")]
        public DateTime DateFrom { get; set; }

        [JsonProperty("dateTo")]
        public DateTime DateTo { get; set; }

        [JsonProperty("reportTypeId")]
        public int ReportTypeId { get; set; }
    }
}
