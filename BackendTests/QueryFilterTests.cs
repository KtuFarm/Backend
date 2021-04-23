using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using BackendTests.Mocks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace BackendTests
{
    public class QueryFilterTests
    {
        private const int DeletedPharmacyId = 2;

        private ApiContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApiContextMock();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public void TestQueryFilter()
        {
            var pharmacy = _context.Pharmacies.FirstOrDefault(p => p.Id == DeletedPharmacyId);

            IsNull(pharmacy);
        }

        [Test]
        public async Task TestIgnoreQueryFilter()
        {
            var pharmacy = await _context.Pharmacies
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == DeletedPharmacyId);

            IsTrue(pharmacy?.IsSoftDeleted);
        }
    }
}
