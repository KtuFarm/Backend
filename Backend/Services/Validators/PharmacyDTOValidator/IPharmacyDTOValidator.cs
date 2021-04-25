using Backend.Models.PharmacyEntity.DTO;
using JetBrains.Annotations;

namespace Backend.Services.Validators.PharmacyDTOValidator
{
    public interface IPharmacyDTOValidator
    {
        [AssertionMethod]
        public void ValidateCreatePharmacyDTO(CreatePharmacyDTO dto);

        [AssertionMethod]
        public void ValidateEditPharmacyDTO(EditPharmacyDTO dto);
    }
}
