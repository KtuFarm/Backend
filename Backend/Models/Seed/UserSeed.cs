using Backend.Models.Database;
using System;
using System.Linq;

namespace Backend.Models.Seed
{
    public class UserSeed
    {
        public static void EnsureCreated(ApiContext context)
        {
            var user = context.Users.FirstOrDefault(p => p.Id == 1);
            if (user != null) return;

            context.Users.AddRange(
                new User
                {
                    Id = 1,
                    Name = "Karolis",
                    Surname = "Balciunas",
                    RegistrationDate = new DateTime(2020, 12, 21),
                    WorkerStateId = WorkerStateId.Working,
                    DismissalDate = null,
                    Position = "Jr. Pharmacist",
                    PharmacyId = 1
                }
                );
        }
    }
}
