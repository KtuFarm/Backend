using System.Linq;
using Backend.Models.Database;

namespace Backend.Models.Seed
{
    public static class ManufacturerSeed
    {
        public static void EnsureCreated(ApiContext context)
        {
            var medicament = context.Manufacturers.FirstOrDefault(m => m.Id == 1);
            if (medicament != null) return;


            context.Manufacturers.AddRange(
                new Manufacturer
                {
                    Id = 1,
                    Country = "Germany",
                    Name = "Bayer GmbH"
                },
                new Manufacturer
                {
                    Id = 2,
                    Country = "Lithuania",
                    Name = "LitPharma"
                },
                new Manufacturer
                {
                    Id = 3,
                    Country = "USA",
                    Name = "Pfizer Inc."
                }
            );
        }
    }
}
