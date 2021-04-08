using System.Linq;
using Backend.Models;
using BackendTests.Mocks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace BackendTests
{
    public class QueryFilterTests
    {
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
            const int id = 1;
            
            DeletePharmacy(id);

            var pharmacy = _context.Pharmacies.FirstOrDefault(p => p.Id == id);
            
            IsNull(pharmacy);
        }

        [Test]
        public void TestIgnoreQueryFilter()
        {
            const int id = 1;
            
            DeletePharmacy(id);

            var pharmacy = _context.Pharmacies.IgnoreQueryFilters().FirstOrDefault(p => p.Id == id);
            
            IsTrue(pharmacy?.IsSoftDeleted);
        }

        private void DeletePharmacy(int id)
        {
            var pharmacy = _context.Pharmacies.FirstOrDefault(p => p.Id == id);
            if (pharmacy != null) pharmacy.IsSoftDeleted = true;

            _context.SaveChanges();
        }
    }
}
