using GenerationReport.Model;

namespace GenerationReport.Implementation
{
    public class WindGeneratorCalculation : GenerationReport.Interface.IGeneratorCalculation
    {
        public string Name { get; set; }
        public Generation Generation { get; set; }
        public string Location { get; set; }

        public double CalculateTotal(ReferenceData referenceData)
        {
            double total = 0;
            double valueFactor = (Location == "Offshore") ? referenceData.Factors.ValueFactor.Low : referenceData.Factors.ValueFactor.High;

            foreach (var day in Generation.Days)
            {
                total += (double)day.Energy * (double)day.Price * valueFactor;
            }

            return total;
        }

        public List<DailyMaxEmission> CalculateDailyEmissions(ReferenceData referenceData)
        {
            return new List<DailyMaxEmission>();
        }

        public CoalHeatRate CalculateHeatRate()
        {
            return null;
        }
    }
}
