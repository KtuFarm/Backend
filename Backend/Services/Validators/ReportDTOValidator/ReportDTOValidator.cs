using Backend.Models.ReportEntity.DTO;

namespace Backend.Services.Validators.ReportDTOValidator
{
    public class ReportDTOValidator : DTOValidator, IReportDTOValidator
    {
        public void ValidateCreateReportDTO(CreateReportDTO dto)
        {
            ValidateNumberIsPositive(dto.ReportTypeId, "reportTypeId");
        }
    }
}
