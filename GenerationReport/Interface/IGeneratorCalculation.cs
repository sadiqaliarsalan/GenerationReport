using GenerationReport.Model;

namespace GenerationReport.Interface
{
    public interface IGeneratorCalculation
    {
        double CalculateTotal(ReferenceData referenceData);

        List<DailyMaxEmission> CalculateDailyEmissions(ReferenceData referenceData);

        CoalHeatRate CalculateHeatRate(); 
    }
}
