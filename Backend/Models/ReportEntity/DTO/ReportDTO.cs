using Newtonsoft.Json;

namespace Backend.Models.ReportEntity.DTO
{
    public class ReportDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("dateFrom")]
        public string DateFrom { get; set; }

        [JsonProperty("dateTo")]
        public string DateTo { get; set; }

        public ReportDTO(Report report)
        {
            Id = report.Id;
            Code = report.Code;
            DateFrom = report.DateFrom.ToString("O");
            DateTo = report.DateTo.ToString("O");
        }
    }
}
