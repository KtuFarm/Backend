using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendTests.Mocks
{
    public class ApiContextMock : ApiContext
    {
        private static readonly DbContextOptions<ApiContext> DbContextOptions =
            new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase("MockDb")
                .Options;

        public ApiContextMock() : base(DbContextOptions)
        {
            SeedDb();
        }

        private void SeedDb()
        {
            WorkingHoursSeedMock.EnsureCreated(this);
            PharmacySeedMock.EnsureCreated(this);
        }
    }
}
