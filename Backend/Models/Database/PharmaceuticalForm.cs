using System.Collections.Generic;
using Backend.Models.MedicamentEntity;

namespace Backend.Models.Database
{
    public enum PharmaceuticalFormId
    {
        Other = 1,
        Tablets,
        Syrup,
        Suspension,
        Lozenge,
        Spray,
        Drops,
        Ointment,
        Injection,
    }

    public class PharmaceuticalForm
    {
        public PharmaceuticalFormId Id { get; set; }

        public string Name { get; set; }

        public ICollection<Medicament> Medicaments { get; set; }
    }
}
