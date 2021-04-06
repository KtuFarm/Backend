using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Database;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class MedicamentDTO
    {
        [JsonProperty("medicamentNo")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("activeSubstance")]
        public string ActiveSubstance { get; set; }

        [JsonProperty("barCode")]
        public string BarCode { get; set; }

        [JsonProperty("isPrescriptionRequired")]
        public bool IsPrescriptionRequired { get; set; }

        [JsonProperty("isReimbursed")]
        public bool IsReimbursed { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("basePrice")]
        public decimal BasePrice { get; set; }

        [JsonProperty("surcharge")]
        public double Surcharge { get; set; }

        [JsonProperty("isSellable")]
        public bool IsSellable { get; set; }

        [JsonProperty("reimbursePercentage")]
        public int ReimbursePercentage { get; set; }

        [JsonProperty("pharmaceuticalForm")] 
        public string PharmaceuticalForm { get; set; }

        public MedicamentDTO(Medicament medicament)
        {
            Id = medicament.Id;
            Name = medicament.Name;
            ActiveSubstance = medicament.ActiveSubstance;
            BarCode = medicament.BarCode;
            IsPrescriptionRequired = medicament.IsPrescriptionRequired;
            IsReimbursed = medicament.IsReimbursed;
            Country = medicament.Country;
            BasePrice = medicament.BasePrice;
            Surcharge = medicament.Surcharge;
            IsSellable = medicament.IsSellable;
            ReimbursePercentage = medicament.ReimbursePercentage;
            PharmaceuticalForm = medicament.PharmaceuticalForm.Name;
        }
    }
}
