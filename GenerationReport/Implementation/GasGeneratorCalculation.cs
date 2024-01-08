using GenerationReport.Model;

namespace GenerationReport.Implementation
{
    public class GasGeneratorCalculation : GenerationReport.Interface.IGeneratorCalculation
    {
        public string Name { get; set; }
        public Generation Generation { get; set; }
        public decimal EmissionsRating { get; set; }

        public double CalculateTotal(ReferenceData referenceData)
        {
            double total = 0;
            double valueFactor = referenceData.Factors.ValueFactor.Medium;

            foreach (var day in Generation.Days)
            {
                total += (double)day.Energy * (double)day.Price * valueFactor;
            }
            return total;
        }

        public List<DailyMaxEmission> CalculateDailyEmissions(ReferenceData referenceData)
        {
            var dailyEmissions = new List<DailyMaxEmission>();
            double emissionsFactor = referenceData.Factors.EmissionsFactor.Medium;

            foreach (var day in Generation.Days)
            {
                var emission = (double)day.Energy * (double)EmissionsRating * emissionsFactor;
                dailyEmissions.Add(new DailyMaxEmission { Name = Name, Date = day.Date, Emission = emission });
            }

            return dailyEmissions;
        }

        public CoalHeatRate CalculateHeatRate()
        {
            return null;
        }
    }
}
