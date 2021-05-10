using System;
using System.Linq;
using Backend.Models.Common;
using Microsoft.EntityFrameworkCore;
using Backend.Models.Database;

namespace Backend.Models.UserEntity
{
    public class UserSeed : ISeeder
    {
        private readonly ApiContext _context;

        public UserSeed(ApiContext context)
        {
            _context = context;
        }

        public void EnsureCreated()
        {
            if (!ShouldSeed()) return;


            _context.Users.AddRange(
                new User
                {
                    Id = 1,
                    Name = "Karolis",
                    Surname = "Balciunas",
                    RegistrationDate = new DateTime(2020, 12, 21),
                    Position = "Jr. Pharmacist",
                    PharmacyId = 1,
                    DepartmentId = DepartmentId.Admin,
                    Email = "test@test.com"
                }
            );

            _context.SaveChanges();
        }

        private bool ShouldSeed()
        {
            var user = _context.Users.IgnoreQueryFilters().FirstOrDefault(m => m.Id == 1);
            return user == null;
        }
    }
}
