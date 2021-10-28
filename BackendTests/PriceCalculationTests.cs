using System.Collections.Generic;
using Backend.Models.MedicamentEntity;
using NUnit.Framework;

namespace BackendTests
{
    public class PriceCalculationTests
    {
        private static IEnumerable<TestCaseData> PriceTestData()
        {
            yield return new TestCaseData(new Medicament
            {
                BasePrice = 20M,
                Surcharge = 10
            }, 22.00M);

            yield return new TestCaseData(new Medicament
            {
                BasePrice = 20M,
                ReimbursePercentage = 10
            }, 18.00M);

            yield return new TestCaseData(new Medicament
            {
                BasePrice = 20M,
                Surcharge = 50,
                ReimbursePercentage = 10
            }, 27.00M);
        }


        [Test, TestCaseSource(nameof(PriceTestData))]
        public void TestPriceCalculation(Medicament m, decimal expectedPrice)
        {
            Assert.AreEqual(expectedPrice, m.CalculatePriceReimbursed());
        }
    }
}
