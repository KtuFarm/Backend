using System;
using System.Linq;
using Backend.Exceptions;
using Backend.Models;
using Backend.Models.Database;
using Backend.Models.DTO;
using Backend.Services.Interfaces;
using JetBrains.Annotations;

namespace Backend.Services
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
            ValidateBasePrice(dto.BasePrice);
            ValidateReimbursePercentage(dto.ReimbursePercentage, "");
        }
        
        public void ValidateEditMedicamentDto(EditMedicamentDTO dto)
        {
            throw new NotImplementedException();
        }

        [AssertionMethod]
        private static void ValidateBarCode(string barcode, string name)
        {
            ValidateString(barcode, name);

            if (!barcode.All(char.IsDigit))
                throw new DtoValidationException(ValidationError.InvalidBarcode);
        }
        
        [AssertionMethod]
        private static void ValidateBasePrice(decimal price)
        {
            if (price <= 0)
                throw new DtoValidationException(ValidationError.InvalidNumber, "basePrice");
        }

        [AssertionMethod]
        private static void ValidatePharmaceuticalForm(int formId)
        {
            if (!Enum.IsDefined(typeof(PharmaceuticalFormId), formId))
                throw new DtoValidationException(ValidationError.InvalidPharmaceuticalForm);
        }
        
        [AssertionMethod]
        private static void ValidateReimbursePercentage(double? percentage, string name)
        {
            if (percentage < 0 || percentage > 100)
                throw new DtoValidationException(ValidationError.InvalidNumber, name);
        }
    }
}
