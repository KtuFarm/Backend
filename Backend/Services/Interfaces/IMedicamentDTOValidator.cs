using Backend.Models.DTO;

namespace Backend.Services.Interfaces
{
    public interface IMedicamentDTOValidator
    {
        public void ValidateCreateMedicamentDto(CreateMedicamentDTO dto);

        public void ValidateEditMedicamentDto(EditMedicamentDTO dto);
    }
}
