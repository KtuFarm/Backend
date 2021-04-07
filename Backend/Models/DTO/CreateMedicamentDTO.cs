﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class CreateMedicamentDTO
    {
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

        [JsonProperty("pharmaceuticalFormId")]
        public int PharmaceuticalFormId { get; set; }
    }

}