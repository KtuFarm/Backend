using Backend.Models.DTO;
using JetBrains.Annotations;

namespace Backend.Services.Interfaces
{
    public interface IPharmacyDTOValidator
    {
        [AssertionMethod]
        public void ValidateCreatePharmacyDTO(CreatePharmacyDTO dto);

        [AssertionMethod]
        public void ValidateEditPharmacyDTO(EditPharmacyDTO dto);
    }
}
