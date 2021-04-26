using Backend.Models.MedicamentEntity.DTO;
using JetBrains.Annotations;

namespace Backend.Services.Validators.MedicamentDTOValidator
{
    public interface IMedicamentDTOValidator
    {
        [AssertionMethod]
        public void ValidateCreateMedicamentDto(CreateMedicamentDTO dto);

        [AssertionMethod]
        public void ValidateEditMedicamentDto(EditMedicamentDTO dto);
    }
}
