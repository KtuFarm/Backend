using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Common;
using Microsoft.EntityFrameworkCore;
using Backend.Models.Database;
using Microsoft.AspNetCore.Identity;

namespace Backend.Models.UserEntity
{
    public class UserSeed : ISeeder
    {
        private readonly ApiContext _context;
        private readonly UserManager<User> _userManager;

        private const string DefaultPassword = "password1";

        public UserSeed(ApiContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureCreated()
        {
            if (!ShouldSeed()) return;

            var users = new[]
            {
                new User
                {
                    Id = 1,
                    Name = "Test",
                    Surname = "Pharmacy",
                    RegistrationDate = new DateTime(2021, 05, 15),
                    Position = "Test User",
                    PharmacyId = 1,
                    DepartmentId = DepartmentId.Pharmacy,
                    Email = "pharmacy@ktufarm.lt",
                    Username = "Test_Pharmacy"
                },
                new User
                {
                    Id = 2,
                    Name = "Test",
                    Surname = "Warehouse",
                    RegistrationDate = new DateTime(2021, 05, 15),
                    Position = "Test User",
                    WarehouseId = 1,
                    DepartmentId = DepartmentId.Warehouse,
                    Email = "warehouse@ktufarm.lt",
                    Username = "Test_Warehouse"
                },
                new User
                {
                    Id = 3,
                    Name = "Test",
                    Surname = "Admin",
                    RegistrationDate = new DateTime(2021, 05, 15),
                    Position = "Test User",
                    DepartmentId = DepartmentId.Admin,
                    Email = "admin@ktufarm.lt",
                    Username = "Test_Admin"
                },
                new User
                {
                    Id = 4,
                    Name = "Test",
                    Surname = "Transportation",
                    RegistrationDate = new DateTime(2021, 05, 15),
                    Position = "Test User",
                    DepartmentId = DepartmentId.Transportation,
                    Email = "transportation@ktufarm.lt",
                    Username = "Test_Transportation"
                },
                new User
                {
                    Id = 5,
                    Name = "Test",
                    Surname = "Manufacturer",
                    RegistrationDate = new DateTime(2021, 05, 15),
                    Position = "Test User",
                    DepartmentId = DepartmentId.Manufacturer,
                    Email = "manufacturer@ktufarm.lt",
                    Username = "Test_Manufacturer"
                }
            };

            foreach (var user in users)
            {
                await _userManager.CreateAsync(user, DefaultPassword);
            }

            await _context.SaveChangesAsync();
        }

        private bool ShouldSeed()
        {
            var user = _context.Users.IgnoreQueryFilters().FirstOrDefault(m => m.Id == 1);
            return user == null;
        }
    }
}
