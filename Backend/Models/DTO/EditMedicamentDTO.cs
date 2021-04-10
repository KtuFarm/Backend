using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Backend.Models.DTO
{
    public class EditMedicamentDTO
    {
        [JsonProperty("isPrescriptionRequired")]
        public bool? IsPrescriptionRequired { get; set; }

        [JsonProperty("isReimbursed")]
        public bool? IsReimbursed { get; set; }

        [JsonProperty("basePrice")]
        public decimal? BasePrice { get; set; }

        [JsonProperty("surcharge")]
        public double? Surcharge { get; set; }

        [JsonProperty("isSellable")]
        public bool? IsSellable { get; set; }

        [JsonProperty("reimbursePercentage")]
        public int? ReimbursePercentage { get; set; }
    }
}
