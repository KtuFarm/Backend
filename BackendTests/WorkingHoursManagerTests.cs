using Backend.Models;
using Backend.Models.DTO;
using Backend.Services;
using BackendTests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NUnit.Framework.Assert;

namespace BackendTests
{
    public class WorkingHoursManagerTests
    {
        private ApiContext _context;
        private WorkingHoursManager _manager;

        [SetUp]
        public void SetUp()
        {
            _context = new ApiContextMock();
            _manager = new WorkingHoursManager(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public void TestSeed()
        {
            int count = _context.WorkingHours.Count();

            AreEqual(WorkingHoursSeedMock.Count, count);
        }

        [Test]
        public void TestInvalidWorkingHours()
        {
            var dto = new List<WorkingHoursDTO>
            {
                new() { OpenTime = "15:00", CloseTime = "12:00", DayOfWeek = 3 }
            };

            AssertInvalidArgument(dto, "Invalid working hours!");
        }

        [Test]
        public void TestInvalidWeekDay()
        {
            var dto = new List<WorkingHoursDTO>
            {
                new() { OpenTime = "10:00", CloseTime = "12:00", DayOfWeek = 10 }
            };

            AssertInvalidArgument(dto, "Invalid day of the week!");
        }

        [Test]
        public void TestUniqueWeekDays()
        {
            var dto = new List<WorkingHoursDTO>
            {
                new() { OpenTime = "09:10", CloseTime = "12:00", DayOfWeek = 2 },
                new() { OpenTime = "10:00", CloseTime = "12:00", DayOfWeek = 2 }
            };

            AssertInvalidArgument(dto, "Days of week should be unique!");
        }

        private void AssertInvalidArgument(List<WorkingHoursDTO> dto, string errorMessage)
        {
            var ex = Throws<ArgumentException>(() => _manager.GetWorkingHoursFromDTO(dto));
            AreEqual(errorMessage, ex.Message);
        }

        [Test]
        public void TestWorkingHoursInContext()
        {
            var dto = new List<WorkingHoursDTO>
            {
                new() { OpenTime = "09:00", CloseTime = "18:00", DayOfWeek = 1 },
                new() { OpenTime = "09:00", CloseTime = "18:00", DayOfWeek = 2 }
            };

            var wh = _manager.GetWorkingHoursFromDTO(dto);

            AreEqual(dto.Count, wh.Count);
            AreEqual(WorkingHoursSeedMock.Count, _context.WorkingHours.Count());
        }

        [Test]
        public void TestNewWorkingHours()
        {
            var dto = new List<WorkingHoursDTO>
            {
                new() { OpenTime = "09:00", CloseTime = "15:00", DayOfWeek = 6 },
                new() { OpenTime = "09:00", CloseTime = "13:00", DayOfWeek = 7 }
            };

            var wh = _manager.GetWorkingHoursFromDTO(dto);

            AreEqual(dto.Count, wh.Count);
            AreEqual(WorkingHoursSeedMock.Count + dto.Count, _context.WorkingHours.Count());
        }

        [Test]
        public async Task TestPharmacyWorkingHours()
        {
            int id = _context.Pharmacies
                .Where(p => p.Id == 1)
                .Select(p => p.Id)
                .FirstOrDefault();

            var wh = await _manager.GetPharmacyWorkingHours(id);

            AreEqual(PharmacySeedMock.PharmacyWorkingHoursCount, wh.Count());
        }
    }
}
