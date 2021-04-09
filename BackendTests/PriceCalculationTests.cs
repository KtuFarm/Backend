using Backend.Models.Database;
using NUnit.Framework;

namespace BackendTests
{
    public class PriceCalculationTests
    {
        [Test]
        public void TestBasePriceWithSurcharge()
        {
            var m = new Medicament
            {
                BasePrice = 20M,
                Surcharge = 10
            };
            
            Assert.AreEqual(m.CalculatePriceReimbursed(), 22);
        }
        
        [Test]
        public void TestBasePriceWithReimburse()
        {
            var m = new Medicament
            {
                BasePrice = 20M,
                ReimbursePercentage = 10
            };
            
            Assert.AreEqual(m.CalculatePriceReimbursed(), 18);
        }

        [Test]
        public void TestPriceWithSurchargeAndReimburse()
        {
            var m = new Medicament
            {
                BasePrice = 20M,
                Surcharge = 50,
                ReimbursePercentage = 10
            };
            
            Assert.AreEqual(m.CalculatePriceReimbursed(), 27);
        }
    }
}
