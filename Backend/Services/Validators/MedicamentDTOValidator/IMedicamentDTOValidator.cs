using Backend.Models.DTO;
using JetBrains.Annotations;

namespace Backend.Services.Interfaces
{
    public interface IMedicamentDTOValidator
    {
        [AssertionMethod]
        public void ValidateCreateMedicamentDto(CreateMedicamentDTO dto);

        [AssertionMethod]
        public void ValidateEditMedicamentDto(EditMedicamentDTO dto);
    }
}
