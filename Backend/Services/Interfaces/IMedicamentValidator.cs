using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.DTO;

namespace Backend.Services.Interfaces
{
    public interface IMedicamentDTOValidator
    {
        public (bool, string ) IsValid(CreateMedicamentDTO dto);
        public (bool, string) IsValid(EditMedicamentDTO dto);
    }
}
