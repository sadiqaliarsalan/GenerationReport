using GenerationReport.Implementation;
using GenerationReport.Model;

namespace GenerationReport.Test.Implementation
{
    [TestFixture]
    public class CoalGeneratorCalculationTest
    {
        [Test]
        public void CalculateTotal_ShouldReturnExpectedTotal_Test()
        {
            var coalGenerator = new CoalGeneratorCalculation
            {
                Generation = new Generation
                {
                    Days = new List<Day>
                    {
                        new Day { Date = new DateTime(2021, 1, 1), Energy = 100m, Price = 50m },
                        new Day { Date = new DateTime(2021, 1, 2), Energy = 200m, Price = 50m }
                    }
                }
            };

            var referenceData = new ReferenceData
            {
                Factors = new Factors
                {
                    ValueFactor = new Factor { Medium = 1.0 }
                }
            };

            double expectedTotal = 100 * 50 + 200 * 50;

            double actualTotal = coalGenerator.CalculateTotal(referenceData);

            Assert.AreEqual(expectedTotal, actualTotal);
        }
    }
}
