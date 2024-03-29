﻿using Backend.Models.PharmacyEntity.DTO;

namespace Backend.Services.Validators.PharmacyDTOValidator
{
    public class PharmacyDTOValidator : DTOValidator, IPharmacyDTOValidator
    {
        public void ValidateCreatePharmacyDTO(CreatePharmacyDTO dto)
        {
            ValidateString(dto.Address, "address");
            ValidateString(dto.City, "city");
            ValidateNumberIsPositive(dto.RegistersCount, "registersCount");
            ValidateListNotEmpty(dto.WorkingHours, "workingHours");
        }

        public void ValidateEditPharmacyDTO(EditPharmacyDTO dto)
        {
            if (dto.Address != null) ValidateString(dto.Address, "address");
            if (dto.City != null) ValidateString(dto.City, "city");
            if (dto.WorkingHours != null) ValidateListNotEmpty(dto.WorkingHours, "workingHours");
        }
    }
}
