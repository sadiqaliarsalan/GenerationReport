using GenerationReport.Interface;
using GenerationReport.Model;

namespace GenerationReport.Service
{
    public class GeneratorCalculationService
    {
        public List<GeneratorTotal> CalculateTotals(List<IGeneratorCalculation> generators, ReferenceData referenceData)
        {
            var totals = new List<GeneratorTotal>();
            foreach (var generator in generators)
            {
                var total = new GeneratorTotal
                {
                    Name = GetName(generator),
                    Total = generator.CalculateTotal(referenceData)
                };
                totals.Add(total);
            }
            return totals;
        }

        public List<DailyMaxEmission> CalculateDailyMaxEmissions(List<IGeneratorCalculation> generators, ReferenceData referenceData)
        {
            var emissions = new List<DailyMaxEmission>();
            foreach (var generator in generators)
            {
                emissions.AddRange(generator.CalculateDailyEmissions(referenceData));
            }

            if (emissions.Any())
            {
                return emissions.GroupBy(e => e.Date)
                .Select(group => group.OrderByDescending(e => e.Emission).FirstOrDefault())
                .ToList();
            }

            return null;
        }

        public List<CoalHeatRate> CalculateHeatRates(List<IGeneratorCalculation> coalGenerators)
        {
            return coalGenerators.Select(generator => generator.CalculateHeatRate()).ToList();
        }

        private string GetName(IGeneratorCalculation generator)
        {
            switch (generator)
            {
                case WindGenerator windGenerator:
                    return windGenerator.Name;
                case GasGenerator gasGenerator:
                    return gasGenerator.Name;
                case CoalGenerator coalGenerator:
                    return coalGenerator.Name;
                default:
                    return "Unknown";
            }
        }
    }
}
