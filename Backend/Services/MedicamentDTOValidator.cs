using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Backend.Models.DTO;
using Backend.Services.Interfaces;
using Org.BouncyCastle.Asn1.Anssi;
using Ubiety.Dns.Core.Records.NotUsed;

namespace Backend.Services
{
    public class MedicamentDTOValidator : IMedicamentDTOValidator
    {
        public (bool, string) IsValid(CreateMedicamentDTO dto)
        {
            // if (string.IsNullOrEmpty(dto.Name)) return (false, "Name is empty!");
            // if (string.IsNullOrEmpty(dto.ActiveSubstance)) return (false, "ActiveSubstance is empty!");
            // if (string.IsNullOrEmpty(dto.BarCode)) return (false, "BarCode is empty!");
            // if (dto.PharmaceuticalFormId == null) return (false, "PharmaceuticalFormIS is empty");
            // if (string.IsNullOrEmpty(dto.Country)) return (false, "Country is empty!");
            //
            // (bool, string) ans = IsValid(dto as EditMedicamentDTO);
            // if (!ans.Item1) return ans;
            //
            return (true, null);
        }

        public (bool, string) IsValid(EditMedicamentDTO dto)
        {
            // string errorMessage = "";
            //
            // if (dto.IsPrescriptionRequired == null)
            //     return (false, "IsPrescriptionRequired is empty");
            // if (dto.IsReimbursed == null)
            //     return (false, "IsReimbursed is empty");
            // if (dto.BasePrice == null)
            //     return (false, "BasePrice is empty!");
            // if (dto.Surcharge == null)
            //     return (false, "Surcharge is empty!");
            // if (dto.IsSellable == null)
            //     return (false, "IsSellable is empty!");
            // if (dto.ReimbursePercentage == null)
            //     return (false, "ReimbursePercentage is empty!");

            return (true, null);
        }


    }
}
