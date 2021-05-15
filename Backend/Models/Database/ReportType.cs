using Backend.Models.ReportEntity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Database
{
    public enum ReportTypeId
    {
        Daily = 1,
        Weekly,
        Custom
    }

    public class ReportType
    {
        [Key]
        [Required]
        public ReportTypeId Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Report> Reports { get; set; }
    }
}
