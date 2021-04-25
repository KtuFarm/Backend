using System;
using System.Linq;
using Backend.Exceptions;
using Backend.Models.Common;
using Backend.Models.Database;
using Backend.Models.MedicamentEntity.DTO;
using JetBrains.Annotations;

namespace Backend.Services.Validators.MedicamentDTOValidator
{
    public class MedicamentDTOValidator : DTOValidator, IMedicamentDTOValidator
    {
        public void ValidateCreateMedicamentDto(CreateMedicamentDTO dto)
        {
            ValidateString(dto.Name, "name");
            ValidateString(dto.ActiveSubstance, "activeSubstance");
            ValidateBarCode(dto.BarCode, "barCode");
            ValidatePharmaceuticalForm(dto.PharmaceuticalFormId);
            ValidateString(dto.Country, "country");
            ValidateNumberIsPositive((double) dto.BasePrice, "basePrice");
            ValidateNumberIsPositive(dto.Surcharge, "surcharge");
            ValidateReimbursePercentage(dto.ReimbursePercentage);
        }

        public void ValidateEditMedicamentDto(EditMedicamentDTO dto)
        {
            if (dto.BasePrice != null) ValidateNumberIsPositive((double) dto.BasePrice, "basePrice");
            if (dto.Surcharge != null) ValidateNumberIsPositive((double) dto.Surcharge, "surcharge");
            if (dto.ReimbursePercentage != null) ValidateReimbursePercentage(dto.ReimbursePercentage);
        }

        [AssertionMethod]
        private static void ValidateBarCode(string barcode, string name)
        {
            ValidateString(barcode, name);

            if (!barcode.All(char.IsDigit))
                throw new DtoValidationException(ApiErrorSlug.InvalidBarcode);
        }

        [AssertionMethod]
        private static void ValidatePharmaceuticalForm(int formId)
        {
            if (!Enum.IsDefined(typeof(PharmaceuticalFormId), formId))
                throw new DtoValidationException(ApiErrorSlug.InvalidPharmaceuticalForm);
        }

        [AssertionMethod]
        private static void ValidateReimbursePercentage(double? percentage, string name = "")
        {
            if (percentage < 0 || percentage > 100)
                throw new DtoValidationException(ApiErrorSlug.InvalidNumber, name);
        }
    }
}
