using Backend.Models.ReportEntity.DTO;
using JetBrains.Annotations;

namespace Backend.Services.Validators.ReportDTOValidator
{
    public interface IReportDTOValidator
    {
        [AssertionMethod]
        public void ValidateCreateReportDTO(CreateReportDTO dto);
    }
}
